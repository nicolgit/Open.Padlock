using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nicold.Padlock.Models;

namespace nicold.Padlock.Models.Services
{
    public class MockItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
    }

    public class MockDataStore : IDataStore<MockItem>
    {
        List<MockItem> items;

        public MockDataStore()
        {
            items = new List<MockItem>();
            var mockItems = new List<MockItem>
            {
                new MockItem { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new MockItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new MockItem { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new MockItem { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new MockItem { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new MockItem { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(MockItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(MockItem item)
        {
            var oldItem = items.Where((MockItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((MockItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<MockItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<MockItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> ClearAllItemAsync()
        {
            items.Clear();

            return await Task.FromResult(true);
        }
    }
}