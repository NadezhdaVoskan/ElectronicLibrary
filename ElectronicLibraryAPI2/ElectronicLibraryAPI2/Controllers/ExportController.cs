using ElectronicLibraryAPI2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ElectronicLibraryAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        public class BackupRequest
        {
            public string SavePath { get; set; }
            public string FileName { get; set; }
        }

        [HttpPost]
        [Route("backupdatabase")]
        public IActionResult BackupDatabase([FromBody] BackupRequest request)
        {
            try
            {
                var connectionString = "Data Source=LAPTOP-MR4G4705\\MYSEVERNAME;Initial Catalog=Library_Database;User ID=sa;Password=123";
                var backupPath = Path.Combine(request.SavePath);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = $"BACKUP DATABASE Library_Database TO DISK = '{backupPath}'";
                    command.ExecuteNonQuery();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("restoredatabase")]
        public IActionResult RestoreDatabase([FromBody] BackupRequest request)
        {
            try
            {
                var connectionString = "Data Source=LAPTOP-MR4G4705\\MYSEVERNAME;Initial Catalog=master;User ID=sa;Password=123";
                var backupPath = Path.Combine(request.SavePath);


                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = $"USE master; RESTORE DATABASE Library_Database FROM DISK = '{backupPath}' WITH REPLACE";
                    command.ExecuteNonQuery();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
