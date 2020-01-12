﻿using DrugKeeper.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrugKeeper.Services
{
    public class MongoRepo
    {
        private readonly IMongoCollection<Pill> PillCollection;

        private readonly IMongoCollection<Reminder> ReminderCollection;

        private readonly IMongoCollection<User> UserCollection;

        public User LoggedUser;

        private readonly HttpClient httpClient;

        public MongoRepo()
        {
            httpClient = new HttpClient();
            var client = new MongoClient("mongodb+srv://homeworkadmin:homeworkadmin@cluster0-xlkgc.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("Samples");
            this.PillCollection = database.GetCollection<Pill>("Pills");
            this.ReminderCollection = database.GetCollection<Reminder>("Reminders");
            this.UserCollection = database.GetCollection<User>("Users");
        }

        public OnlinePharmacy GetOnlinePharmacy()
        {
            DateTime today = DateTime.Now;
            int day = today.Day;
            int month = today.Month;
            int year = today.Year;

            
            string dayString = day + "-0" + month + "-" + year;

            var requestLink = "http://eskisehireo.sosyalinteraktif.com/tr/default/nec/harita-nobet-listesi-data?date=" + dayString;

            var response = httpClient.GetAsync(requestLink).Result;

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var test = JsonConvert.DeserializeObject<OnlinePharmacy>(content);
                return test;
            }

            return null;
        }

        public void AddReminder(Reminder newReminder)
        {
            if (LoggedUser != null)
            {
                LoggedUser.Reminders.Add(newReminder);

                this.UserCollection.ReplaceOne<User>(u => u.Username == LoggedUser.Username, LoggedUser);
            }
        }

        public void AddPill(Pill pill)
        {
            try
            {
                pill.Id = Guid.NewGuid();
                //look for if there is any medicine with the same name
                if (this.PillCollection.Find(p => p.Name == pill.Name).FirstOrDefault() == null)
                    this.PillCollection.InsertOne(pill);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<List<Reminder>> GetAllRemindersAsync(string userName)
        {
            var reminders = await GetUserAsync(userName);

            return reminders.Reminders;
        }

        public async Task<List<Pill>> GetAllPills()
        {
            var pills = await this.PillCollection.Find(pill => true).ToListAsync();
            return pills;
        }

        public List<User> GetAllUsers()
        {
            return this.UserCollection.Find(user => true).ToList();
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await this.UserCollection.Find<User>(user => user.Username == username).FirstOrDefaultAsync();
        }

        public void RegisterUser(User user)
        {
            this.UserCollection.InsertOne(user);
        }

        public bool CheckUser(User user)
        {
            var checkedUser = this.UserCollection.Find(x => x.Username == user.Username && x.Password == user.Password).First();

            if (checkedUser == null)
                return false;

            return true;
        }
    }
}
