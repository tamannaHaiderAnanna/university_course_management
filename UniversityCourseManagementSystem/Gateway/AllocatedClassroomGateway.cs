using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using iTextSharp.xmp.impl;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Gateway
{
    public class AllocatedClassroomGateway : BaseGateway
    {
        public List<AllocatedClassroom> GetAllRooms()
        {
            string query = "SELECT * FROM Room";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<AllocatedClassroom> allRooms = new List<AllocatedClassroom>();
            while (Reader.Read())
            {
                AllocatedClassroom aRoom = new AllocatedClassroom();
                aRoom.RoomId = (int)Reader["Id"];
                aRoom.RoomNo = Reader["RoomNo"].ToString();
                allRooms.Add(aRoom);
            }

            Reader.Close();
            Connection.Close();
            return allRooms;
        }

        public List<string> GetAllDays()
        {
            List<string> days = new List<string>()
            {
                "Saturday","Sunday","Monday","Tuesday","Wednesday","Thursday","Friday"
            };
            return days;
        }
        // Saves the booking only if the room is free on that day for the whole time, in one transaction.
        // Returns false (and saves nothing) if it overlaps a booking already made for that room and day.
        public bool SaveIfRoomFree(AllocatedClassroom aClassroom, TimeSpan fromTime, TimeSpan toTime)
        {
            Connection.Open();
            SqlTransaction transaction = Connection.BeginTransaction();
            try
            {
                // Same room, same day, and the times overlap (each one starts before the other ends). Stored
                // times are text as the page sent them, so they are turned into TIME before comparing.
                // UPDLOCK + HOLDLOCK keep that room and day locked until the commit, so two people can't book
                // the same slot at the same moment.
                string query = "SELECT COUNT(*) FROM AllocatedRooms WITH (UPDLOCK, HOLDLOCK) " +
                               "WHERE RoomId = @roomId AND Day = @day AND Status = 1 " +
                               "AND TRY_CAST(StartTime AS TIME) < @toTime AND TRY_CAST(FinishTime AS TIME) > @fromTime";

                Command = new SqlCommand(query, Connection, transaction);

                Command.Parameters.Clear();
                Command.Parameters.Add("roomId", SqlDbType.Int);
                Command.Parameters["roomId"].Value = aClassroom.RoomId;

                Command.Parameters.Add("day", SqlDbType.VarChar);
                Command.Parameters["day"].Value = aClassroom.Day;

                Command.Parameters.Add("fromTime", SqlDbType.Time);
                Command.Parameters["fromTime"].Value = fromTime;

                Command.Parameters.Add("toTime", SqlDbType.Time);
                Command.Parameters["toTime"].Value = toTime;

                int overlapping = (int)Command.ExecuteScalar();
                if (overlapping > 0)
                {
                    RollbackAndClose(transaction);
                    return false;
                }

                query = "INSERT INTO AllocatedRooms VALUES(@departmentId,@courseId,@roomId,@day,@startTime,@finishTime,@status)";

                Command = new SqlCommand(query, Connection, transaction);

                Command.Parameters.Clear();
                Command.Parameters.Add("departmentId", SqlDbType.Int);
                Command.Parameters["departmentId"].Value = aClassroom.DepartmentId;

                Command.Parameters.Add("courseId", SqlDbType.Int);
                Command.Parameters["courseId"].Value = aClassroom.CourseId;

                Command.Parameters.Add("roomId", SqlDbType.Int);
                Command.Parameters["roomId"].Value = aClassroom.RoomId;

                Command.Parameters.Add("day", SqlDbType.VarChar);
                Command.Parameters["day"].Value = aClassroom.Day;

                Command.Parameters.Add("startTime", SqlDbType.VarChar);
                Command.Parameters["startTime"].Value = aClassroom.FromTime;

                Command.Parameters.Add("finishTime", SqlDbType.VarChar);
                Command.Parameters["finishTime"].Value = aClassroom.ToTime;

                Command.Parameters.Add("status", SqlDbType.Bit);
                Command.Parameters["status"].Value = aClassroom.Status;

                Command.ExecuteNonQuery();

                transaction.Commit();
                Connection.Close();

                return true;
            }
            catch
            {
                RollbackAndClose(transaction);
                throw;
            }
        }
        public List<ViewScheduleInfo> GetAllInfo()
        {
            string query = "SELECT * FROM GetScheduleInfo";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<ViewScheduleInfo> allInfo = new List<ViewScheduleInfo>();
            while (Reader.Read())
            {
                ViewScheduleInfo scheduleInfo = new ViewScheduleInfo();

                scheduleInfo.DepartmentId = (int) Reader["Id"];
                scheduleInfo.CourseCode = Reader["CourseCode"].ToString();
                scheduleInfo.CourseName = Reader["CourseName"].ToString();
                scheduleInfo.ScheduleInfo = Reader["Schedule Info"].ToString();
                scheduleInfo.Status = Reader["Status"].ToString();
                
                    if (scheduleInfo.Status == "" || scheduleInfo.Status == "False")
                    {
                        scheduleInfo.ScheduleInfo = "Not Schedule Yet";
                    }
               
                allInfo.Add(scheduleInfo);
            }
            
            Reader.Close();
            Connection.Close();

            return allInfo;
        }

        public int UnallocatedClassrooms()
        {
            string query = "DELETE FROM AllocatedRooms";

            Command = new SqlCommand(query, Connection);
            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();

            return rowAffected;
        }

        //public int UpdateStatus(AllocatedClassroom aClassroom)
        //{
        //    string query = "UPDATE AllocatedRooms SET Status= '" + true + "',StartTime='"+aClassroom.FromTime+"',FinishTime='"+aClassroom.ToTime+"' " +
        //                   " WHERE (CourseId = '"+aClassroom.CourseId+"' AND DepartmentId = '"+aClassroom.DepartmentId+"')";
        //    Command = new SqlCommand(query, Connection);
        //    Connection.Open();

        //    int rowAffected = Command.ExecuteNonQuery();
        //    Connection.Close();

        //    return rowAffected;
        //}
        //public int UpdateSameCourse(AllocatedClassroom aClassroom)
        //{
        //    string query = "UPDATE AllocatedRooms SET Status= '" + true + "' " +
        //                   "WHERE (CourseId = '" + aClassroom.CourseId + "' AND DepartmentId = '" + aClassroom.DepartmentId + "') ";
        //    Command = new SqlCommand(query, Connection);
        //    Connection.Open();

        //    int rowAffected = Command.ExecuteNonQuery();
        //    Connection.Close();

        //    return rowAffected;
        //}
    }
}