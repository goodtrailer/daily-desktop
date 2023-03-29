// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System.Threading.Tasks;
using DailyDesktop.Core.Configuration;
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
            var wallpaperConfig = new WallpaperConfiguration("");
            await new BingProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestCalvinAndHobbes()
        {
            var wallpaperConfig = new WallpaperConfiguration("");
            await new CalvinAndHobbesProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
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
            var wallpaperConfig = new WallpaperConfiguration("");
            await new FalseKneesProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
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
            var wallpaperConfig = new WallpaperConfiguration("");
            await new PixivProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestPokemon()
        {
            var wallpaperConfig = new WallpaperConfiguration("");
            await new PokemonProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestRedditEarthPorn()
        {
            var wallpaperConfig = new WallpaperConfiguration("");
            await new RedditEarthPornProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestUnsplash()
        {
            var wallpaperConfig = new WallpaperConfiguration("");
            await new UnsplashProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

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
            var wallpaperConfig = new WallpaperConfiguration("");
            await new WikimediaCommonsProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Author: " + wallpaperConfig.Author);
            TestContext.WriteLine("Author URI: " + wallpaperConfig.AuthorUri);
            TestContext.WriteLine("Description: " + wallpaperConfig.Description);
            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Description), "Null/whitespace description.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public async Task TestXkcd()
        {
            var wallpaperConfig = new WallpaperConfiguration("");
            await new XkcdProvider().ConfigureWallpaper(wallpaperConfig);

            TestContext.WriteLine("Image URI: " + wallpaperConfig.ImageUri);
            TestContext.WriteLine("Title: " + wallpaperConfig.Title);
            TestContext.WriteLine("Title Uri: " + wallpaperConfig.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaperConfig.TitleUri), "Null/whitespace title URI.");
        }
    }
}
