using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrugKeeper.Models;

namespace DrugKeeper.Services
{
    public class MockDataStore : IDataStore<Pill>
    {
        readonly List<Pill> items;

        public MockDataStore()
        {
            //items = new List<Pill>()
            //{
            //    new Pill { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
            //    new Pill { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
            //    new Pill { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
            //    new Pill { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
            //    new Pill { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
            //    new Pill { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            //};
        }

        public async Task<bool> AddItemAsync(Pill item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Pill item)
        {
            var oldItem = items.Where((Pill arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Pill arg) => arg.Id.Equals(Guid.Parse(id))).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Pill> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.Equals(Guid.Parse(id))));
        }

        public async Task<IEnumerable<Pill>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}