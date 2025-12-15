// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using DailyDesktop.Core.Util;
using DailyDesktop.Providers.Bing;
using DailyDesktop.Providers.CalvinAndHobbes;
using DailyDesktop.Providers.DeviantArt;
using DailyDesktop.Providers.MTG;
using DailyDesktop.Providers.Pixiv;
using DailyDesktop.Providers.RedditEarthPorn;
using DailyDesktop.Providers.Unsplash;
using DailyDesktop.Providers.WikimediaCommons;
using DailyDesktop.Providers.Xkcd;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyDesktop.Tests
{
    [TestClass]
    public class TestProviders
    {
        public TestContext TestContext { get; set; } = null!;

        [TestMethod]
        public async Task TestBing()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new BingProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestCalvinAndHobbes()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new CalvinAndHobbesProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestDeviantArt()
        {
            if (Environment.GetEnvironmentVariable("CI") == "true")
                Assert.Inconclusive("Skipped due to CI testing.");

            var wallpaperConfig = new WallpaperConfiguration();
            await new DeviantArtProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestMTG()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new MTGProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public async Task TestPixiv()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new PixivProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestRedditEarthPorn()
        {
            if (Environment.GetEnvironmentVariable("CI") == "true")
                Assert.Inconclusive("Skipped due to CI testing.");

            var wallpaperConfig = new WallpaperConfiguration();
            await new RedditEarthPornProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestUnsplash()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new UnsplashProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestWikimediaCommons()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new WikimediaCommonsProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);
            
            // The description can also be unreliable. Since we're really just testing that the API works,
            // we only really need title+image (the bare necessities).
            // Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            // Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            // Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestXkcd()
        {
            var wallpaperConfig = new WallpaperConfiguration();
            await new XkcdProvider().ConfigureWallpaperAsync(wallpaperConfig, AsyncUtils.LongCancel());

            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title URI: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }
    }
}
