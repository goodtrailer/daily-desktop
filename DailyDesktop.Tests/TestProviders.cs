// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading.Tasks;
using DailyDesktop.Core.Providers;
using DailyDesktop.Providers.Bing;
using DailyDesktop.Providers.CalvinAndHobbes;
using DailyDesktop.Providers.FalseKnees;
using DailyDesktop.Providers.Pixiv;
using DailyDesktop.Providers.Pokemon;
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
            var provider = new BingProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestCalvinAndHobbes()
        {
            var provider = new CalvinAndHobbesProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        // [TestMethod]
        // public async Task TestDeviantArt()
        // {
        //     var provider = new DeviantArtProvider();
        //     var wallpaper = await provider.GetWallpaperInfo();

        //     context.WriteLine("Image URI: " + wallpaper.ImageUri);
        //     context.WriteLine("Author: " + wallpaper.Author);
        //     context.WriteLine("Author URI: " + wallpaper.AuthorUri);
        //     context.WriteLine("Title: " + wallpaper.Title);
        //     context.WriteLine("Title Uri: " + wallpaper.TitleUri);
        //     context.WriteLine("Description: " + wallpaper.Description);

        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        // }

        [TestMethod]
        public async Task TestFalseKnees()
        {
            var provider = new FalseKneesProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        // DEPRECATED: See MTGProvider.cs
        // [TestMethod]
        // public async Task TestMTG()
        // {
        //     var provider = new MTGProvider();
        //     var wallpaper = await provider.GetWallpaperInfo();

        //     context.WriteLine("Image URI: " + wallpaper.ImageUri);
        //     context.WriteLine("Author: " + wallpaper.Author);
        //     context.WriteLine("Title: " + wallpaper.Title);
        //     context.WriteLine("Title Uri: " + wallpaper.TitleUri);
        //     context.WriteLine("Description: " + wallpaper.Description);

        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        //     Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        // }

        [TestMethod]
        public async Task TestPixiv()
        {
            var provider = new PixivProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestPokemon()
        {
            var provider = new PokemonProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestRedditEarthPorn()
        {
            var provider = new RedditEarthPornProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestUnsplash()
        {
            var provider = new UnsplashProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestWikimediaCommons()
        {
            var provider = new WikimediaCommonsProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);
            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestXkcd()
        {
            var provider = new XkcdProvider();
            var wallpaper = await provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }
    }
}
