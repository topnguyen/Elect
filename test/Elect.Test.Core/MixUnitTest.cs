﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Elect.Core.CrawlerUtils;
using Elect.Location.Coordinate.PolygonUtils;
using Elect.Location.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class MixUnitTest
    {
        [TestMethod]
        public void BuildCategoryOptionMarix()
        {
            List<string> categoryOptions1 = new List<string> {"c1o1", "c1o2", "c1o3"};

            List<string> categoryOptions2 = new List<string> {"c2o1", "c2o2", "c2o3", "c2o4", "c2o5"};

            List<string> categoryOptions3 = new List<string> {"c3o1", "c3o2", "c3o3"};

            List<string> categoryOptions4 = new List<string> {"c4o1", "c4o2", "c4o3"};

            List<string> categoryOptions5 = new List<string> {"c5o1", "c5o2", "c5o3", "c5o4"};

            List<string> categoryOptions6 = new List<string> {"c6o1", "c6o2", "c6o3"};

            var categoryOptions = new List<List<string>>
            {
                categoryOptions1, categoryOptions2, categoryOptions3, categoryOptions4, categoryOptions5,
                categoryOptions6
            };

            // Build Nested Options

            var listOptionModels = BuildListOptionModel(categoryOptions, 0);

            // Build list Key

            var listKeys = BuildListKey("", listOptionModels);

            Assert.AreEqual(listKeys.Count,
                categoryOptions1.Count * categoryOptions2.Count * categoryOptions3.Count * categoryOptions4.Count *
                categoryOptions5.Count * categoryOptions6.Count);
        }

        public List<OptionModel> BuildListOptionModel(List<List<string>> categoryOptions, int iCategoryOption)
        {
            var listOptionModels = new List<OptionModel>();

            var categoryOption = categoryOptions[iCategoryOption];

            foreach (var option in categoryOption)
            {
                var optionModel = new OptionModel
                {
                    Id = option
                };

                var iNextCategoryOption = iCategoryOption + 1;

                if (iNextCategoryOption < categoryOptions.Count)
                {
                    optionModel.OptionModels = BuildListOptionModel(categoryOptions, iNextCategoryOption);
                }

                listOptionModels.Add(optionModel);
            }

            return listOptionModels;
        }

        public List<string> BuildListKey(string key, List<OptionModel> listOptionModels)
        {
            var keys = new List<string>();

            foreach (var optionModel in listOptionModels)
            {
                var newKey = key + "_" + optionModel.Id;

                if (optionModel.OptionModels.Any())
                {
                    var subKeys = BuildListKey(newKey, optionModel.OptionModels);

                    keys.AddRange(subKeys);
                }
                else
                {
                    newKey = newKey.Trim('_');
                    keys.Add(newKey);
                }
            }

            return keys;
        }

        public class OptionModel
        {
            public string Id { get; set; }

            public List<OptionModel> OptionModels { get; set; } = new List<OptionModel>();
        }

        [TestMethod]
        public async Task CrawlMetadataTestCase()
        {
            var metadataModels = await CrawlerHelper.GetListMetadataAsync(
                // "https://t.co/aLowoOrIQJ?amp=1"
                "https://factcheck.afp.com/these-photos-show-2012-passenger-plane-crash-myanmars-shan-state"
                );

            var html = @"<!DOCTYPE html><html lang=en><head><link href='https://fonts.googleapis.com/css?family=Cabin+Condensed:400,500,700|Muli:400' rel=stylesheet><title>Top Nguyen</title><link rel=publisher href=https://facebook.com/topnguyen.net><link rel=author href=https://facebook.com/topnguyen.net><meta property=og:see_also content='https://topnguyen.com/'><meta property=og:site_name content='Top Nguyen'><meta charset=utf-8><meta http-equiv=X-UA-Compatible content='text/html; charset=UTF-8; IE=edge'><meta name=viewport content='width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=no, minimal-ui'><meta name=apple-mobile-web-app-capable content=yes><meta name=apple-mobile-web-app-status-bar-style content=black-translucent><meta name=apple-mobile-web-app-title content='Top Nguyen'><meta name=application-name content='Top Nguyen'><meta property=og:locale content=en_US><meta property=og:locale:alternate content=vi_VN><meta property=og:site_name content='Top Nguyen'><meta name=twitter:site content=@topnguyen><meta property=fb:app_id content=122116614877437><meta property=fb:admins content=100006201966137><meta name=msvalidate.01 content=9EDF9E6C8DF34B5CAA5FFBF0943F43DF><meta name=viewport content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0'><meta name=HandheldFriendly content=True><meta name=referrer content=origin><meta name=robots content=noodp><link rel=canonical href='https://topnguyen.com/'><meta itemprop=name><meta itemprop=description content='Hi. I am Top Nguyen, I am C# .NET Senior Developer. I do the work I do because I love it.'><meta itemprop=image content=https://topnguyen.com/Assets/Images/profile/topnguyen-with-background.png><link rel=image_src href=https://topnguyen.com/Assets/Images/profile/topnguyen-with-background.png><meta property=og:title><meta property=og:image content=https://topnguyen.com/Assets/Images/profile/topnguyen-with-background.png><meta property=og:type content=website><meta property=og:url content='https://topnguyen.com/'><meta property=og:description content='Hi. I am Top Nguyen, I am C# .NET Senior Developer. I do the work I do because I love it.'><meta name=description content='Hi. I am Top Nguyen, I am C# .NET Senior Developer. I do the work I do because I love it.'><meta name=twitter:title><meta name=twitter:card content=summary_large_image><meta name=twitter:image content=https://topnguyen.com/Assets/Images/profile/topnguyen-with-background.png><meta name=twitter:description content='Hi. I am Top Nguyen, I am C# .NET Senior Developer. I do the work I do because I love it.'><meta name=twitter:url content='https://topnguyen.com/'><link rel=apple-touch-icon sizes=57x57 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-57x57.png><link rel=apple-touch-icon sizes=60x60 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-60x60.png><link rel=apple-touch-icon sizes=72x72 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-72x72.png><link rel=apple-touch-icon sizes=76x76 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-76x76.png><link rel=apple-touch-icon sizes=114x114 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-114x114.png><link rel=apple-touch-icon sizes=120x120 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-120x120.png><link rel=apple-touch-icon sizes=144x144 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-144x144.png><link rel=apple-touch-icon sizes=152x152 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-152x152.png><link rel=apple-touch-icon sizes=180x180 href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-180x180.png><link rel=icon type=image/png href=https://topnguyen.com/Assets/Favicons/favicon-32x32.png sizes=32x32><link rel=icon type=image/png href=https://topnguyen.com/Assets/Favicons/android-chrome-192x192.png sizes=192x192><link rel=icon type=image/png href=https://topnguyen.com/Assets/Favicons/favicon-96x96.png sizes=96x96><link rel=icon type=image/png href=https://topnguyen.com/Assets/Favicons/favicon-16x16.png sizes=16x16><link rel='shortcut icon' href=https://topnguyen.com/Assets/Favicons/apple-touch-icon-57x57.png><link rel=manifest href=https://topnguyen.com/Assets/Favicons/manifest.json><link rel=mask-icon href=https://topnguyen.com/Assets/Favicons/safari-pinned-tab.svg color=#5b3693><meta name=msapplication-TileColor content=#5b3693><meta name=msapplication-TileImage content=https://topnguyen.com/Assets/Favicons/mstile-144x144.png><meta name=theme-color content=#ffffff><link href='/styles?v=xZE2TPgbiP6PR5qP732yaw58yGhtk46Wt3DemwIk7xk1' rel=stylesheet><link href=https://afeld.github.io/emoji-css/emoji.css rel=stylesheet><script async defer src=https://www.google.com/recaptcha/api.js></script><script async defer>(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','https://www.google-analytics.com/analytics.js','ga');ga('create','UA-48623223-3','auto');ga('send','pageview');</script><script>!function(){var t;if(t=window.driftt=window.drift=window.driftt||[],!t.init)return t.invoked?void(window.console&&console.error&&console.error('Drift snippet included twice.')):(t.invoked=!0,t.methods=['identify','config','track','reset','debug','show','ping','page','hide','off','on'],t.factory=function(e){return function(){var n;return n=Array.prototype.slice.call(arguments),n.unshift(e),t.push(n),t;};},t.methods.forEach(function(e){t[e]=t.factory(e);}),t.load=function(t){var e,n,o,i;e=3e5,i=Math.ceil(new Date()/e)*e,o=document.createElement('script'),o.type='text/javascript',o.async=!0,o.crossorigin='anonymous',o.src='https://js.driftt.com/include/'+i+'/'+t+'.js',n=document.getElementsByTagName('script')[0],n.parentNode.insertBefore(o,n);});}();drift.SNIPPET_VERSION='0.3.1';drift.load('nnk4vbkt9xd2');</script><script>window['_fs_debug']=false;window['_fs_host']='fullstory.com';window['_fs_org']='5EA7V';window['_fs_namespace']='FS';(function(m,n,e,t,l,o,g,y){if(e in m){if(m.console&&m.console.log){m.console.log('FullStory namespace conflict. Please set window['_fs_namespace'].');}return;}
g=m[e]=function(a,b){g.q?g.q.push([a,b]):g._api(a,b);};g.q=[];o=n.createElement(t);o.async=1;o.src='https://'+_fs_host+'/s/fs.js';y=n.getElementsByTagName(t)[0];y.parentNode.insertBefore(o,y);g.identify=function(i,v){g(l,{uid:i});if(v)g(l,v)};g.setUserVars=function(v){g(l,v)};y='rec';g.shutdown=function(i,v){g(y,!1)};g.restart=function(i,v){g(y,!0)};y='consent';g[y]=function(a){g(y,!arguments.length||a)};g.identifyAccount=function(i,v){o='account';v=v||{};v.acctId=i;g(o,v)};g.clearUserCookie=function(){};})(window,document,window['_fs_namespace'],'script','user');</script><script src=//d2wy8f7a9ursnm.cloudfront.net/v4/bugsnag.min.js></script><script>window.bugsnagClient=bugsnag('e5c5916d46b0194e434781d09c1f90a5')</script><script src=https://cdn.ravenjs.com/3.26.1/raven.min.js crossorigin=anonymous></script><script>Raven.config('https://055b1acbb4254758829a793dbf030b2a@sentry.io/1223344').install();</script><body><div class=menu-section><div class=container><div class=row><div id=launcher-menu class='col-md-12 launcher-nav launcher-nav-background hidden'><div id=row-b class=row><div id=launcher-slider class='swiper-container swiper-container-launcher'><div class=swiper-wrapper><div class=swiper-slide><div class='col-md-12 col-sm-12'><div class=row><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-resharper-license-server data-url=http://license.topnguyen.net class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/key.png class='overlay-icon animated zoomIn' alt='Resharper License Server'> <a href=http://license.topnguyen.net><span class='icon-info animated zoomIn'>Resharper License</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-blog data-url=https://topnguyen.com/blog class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/post-it.png class='overlay-icon animated zoomIn' alt=Blog> <a href=https://topnguyen.com/blog><span class='icon-info animated zoomIn'>Blog</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-home data-url='https://topnguyen.com/' class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/house.png class='overlay-icon animated zoomIn' alt=Home> <a href='https://topnguyen.com/'> <span class='icon-info animated zoomIn'>Home</span> </a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-about data-url=https://topnguyen.com/about-me class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/comvzhssmyverizon.png class='overlay-icon animated zoomIn' alt='About Me'> <a href=https://topnguyen.com/about-me><span class='icon-info animated zoomIn'>About Me</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-skills data-url=https://topnguyen.com/skill class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/pie_chart.png class='overlay-icon animated zoomIn' alt=Skills> <a href=https://topnguyen.com/skill><span class='icon-info animated zoomIn'>Skills</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-education data-url=https://topnguyen.com/education class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/graduation_cap.png class='overlay-icon animated zoomIn' alt=Education> <a href=https://topnguyen.com/education><span class='icon-info animated zoomIn'>Education</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-experience data-url=https://topnguyen.com/experience class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/layers.png class='overlay-icon animated zoomIn' alt=Experience> <a href=https://topnguyen.com/experience><span class='icon-info animated zoomIn'>Experience</span></a> </button></div></div></div></div></div><div class=swiper-slide><div class='col-md-12 col-sm-12'><div class=row><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-subscribe data-url=https://topnguyen.com/subscribe class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/bell.png class='overlay-icon animated zoomIn' alt=Subscribe> <a href=https://topnguyen.com/subscribe></a><span class='icon-info animated zoomIn'>Subscribe</span> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-interest data-url=https://topnguyen.com/interest class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/heart.png class='overlay-icon animated zoomIn' alt=Interest> <span class='icon-info animated zoomIn'>Interest</span> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-contact data-url=https://topnguyen.com/contact class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/envelope.png class='overlay-icon animated zoomIn' alt=Contact> <a href=https://topnguyen.com/contact><span class='icon-info animated zoomIn'>Contact</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-map data-url=https://topnguyen.com/find-me class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/google_maps.png class='overlay-icon animated zoomIn' alt='Find Me'> <a href=https://topnguyen.com/find-me><span class='icon-info animated zoomIn'>Find Me</span></a> </button></div></div><div class='col-md-3 col-sm-3 col-xs-6'><div class=icon-container><button id=launcher-topnguyen-facebook data-url=http://facebook.com/topnguyen.net class='overlay-btn nav-btn'> <img src=/Assets/Images/icons/social-facebook.png class='overlay-icon animated zoomIn' alt=Facebook> <a href=http://facebook.com/topnguyen.net><span class='icon-info animated zoomIn'>Facebook</span></a> </button></div></div></div></div></div></div><div class='swiper-pagination swiper-pagination-launcher animated fadeInUp delay1s'></div></div></div></div></div></div></div><div class=hidden><button id=launcher-topnguyen-blog-detail class='overlay-btn hidden'> <img src=/Assets/Images/icons/reading.png class='overlay-icon animated zoomIn' alt=''> <span class='icon-info animated zoomIn'></span> </button> <button id=launcher-topnguyen-close-post class='overlay-btn hidden'> <img src=/Assets/Images/icons/book.png class='overlay-icon animated zoomIn' alt=''> <span class='icon-info animated zoomIn'>Close Post</span> </button></div><div id=intro class='launch-intro hidden'><div class=launch-content><div class=loading-center><div class=scale-circle><span></span><span></span></div><div class=loader-content></div></div></div></div><div class=section-intro><h5><i class=ion-record></i> Top Nguyen</h5><div class=intro-buttons><button class=btn-subscribe><i class=ion-android-notifications></i></button> <button class=full-screen><i class=ion-arrow-resize></i></button></div></div><div class=super-container><div class=share-wrap><div class=share><a href='https://www.facebook.com/sharer.php?u=https://topnguyen.com/' target=_blank class=pull-share-item title='Share on Facebook'><i class=ion-social-facebook></i></a> <a href='https://topnguyen.com/' class=pull-share-item title=Refresh><i class=ion-refresh></i></a> <a href='https://plus.google.com/share?url=https://topnguyen.com/' target=_blank class=pull-share-item title='Share on Google Plus'><i class=ion-social-googleplus></i></a></div></div><div id=pre-loader class='loading hidden'><div class=loading-center><div class=scale-circle><span></span><span></span></div><div class=loader-content></div></div></div><main class=main><section id=topnguyen-home class=section><div class=container><div class=desktop data-image-bg=/Assets/Images/backgrounds/topnguyen-home.jpg data-image-overlay='rgba(27, 21, 21, 0.4)'><div class=topnguyen-home-content><div class=content><div class=topnguyen-home-holder><div class='profile-image col-md-6'><img src=/Assets/Images/profile/topnguyen.png alt=''></div><div class='profile-block col-md-6'><div class=profile-infos><div class=text-content><h4>HEY!</h4><h4 class=profile-name>I'M <b class=accent-color>TOP NGUYEN</b></h4><h5>Senior Software Engineering in HCMC - Viet Nam</h5><ul class=profile-list><li><span class=title-icon><i class=ion-ios-email-outline aria-hidden=true></i></span> <span class=item-content><a href=mailto:topnguyen92@gmail.com>topnguyen92@gmail.com</a></span><li><span class=title-icon><i class=ion-ios-world-outline aria-hidden=true></i></span> <span class=item-content><a href=http://topnguyen.net>topnguyen.net</a></span><li><span class=title-icon><i class=ion-social-skype-outline aria-hidden=true></i></span> <span class=item-content><a href=skype:live:vodanh_pp?chat>live:vodanh_pp</a></span><li><span class=title-icon><i class=ion-ios-telephone-outline aria-hidden=true></i></span> <span class=item-content><a href=tel:(+84)945188299>(+84)945 188 299</a></span></ul></div><ul class=social-list><li><a href=https://www.facebook.com/topnguyen.net target=_blank><i class=ion-social-facebook-outline></i></a><li><a href=skype:live:vodanh_pp?chat><i class=ion-social-skype-outline></i></a><li><a href=http://vn.linkedin.com/in/topnguyen target=_blank><i class=ion-social-linkedin-outline></i></a></ul></div></div></div></div></div></div></div></section></main></div><nav id=btm-launcher class=nav-launcher><div class=container style=padding:0><div class=nav-bg> <button id=topnguyen-btm-nav-blog data-url=https://topnguyen.com/blog class='dock-btn nav-btn' data-toggle=tooltip data-placement=top title=Blog> <img src=/Assets/Images/icons/post-it.png class='dock-icon animated zoomIn' alt=Blog> <a href=https://topnguyen.com/blog><span class='animated zoomIn'>Blog</span></a> </button> <button id=menu-launcher-btn class=launcher-icon><i class=ion-grid></i></button> <button id=topnguyen-btm-nav-home data-url='https://topnguyen.com/' class='dock-btn nav-btn' data-toggle=tooltip data-placement=top title=Home> <img src=/Assets/Images/icons/house.png class='dock-icon animated zoomIn' alt=Home> <a href='https://topnguyen.com/'><span class='animated zoomIn'>Home</span></a> </button> <button id=topnguyen-btm-nav-cont data-url=https://topnguyen.com/contact class='dock-btn nav-btn' data-toggle=tooltip data-placement=top title=Contact> <img src=/Assets/Images/icons/envelope.png class='dock-icon animated zoomIn' alt=Contact> <a href=https://topnguyen.com/contact><span class='animated zoomIn'>Contact</span></a> </button></div></div></nav><script>var currentUrl='https://topnguyen.com/';var disqus_shortname='topnguyen';</script><script src='/scripts?v=zAsSuTaQYkpuu2BtNjsH6H57x98w53EVdN40vLOpV181'></script><script src='/script-home?v=T-9zLVr5FmB67BhD5CHWcTZZONXsvovnnkjf2B81aLY1'></script><script src='//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-57946039057ed441' async></script><script>var addthis_config=addthis_config||{};addthis_config.data_track_addressbar=false;addthis_config.data_track_clickback=false;</script>
";
            var metaDataModel = await CrawlerHelper.GetMetadataByHtmlAsync(html);
        }

        [TestMethod]
        public void PolygonTestCase()
        {
            var polygon = new List<CoordinateModel>
            {
                new CoordinateModel(103.77819210947298, 1.5149825899642049),
                new CoordinateModel(103.79741818369173, 1.4888990253023116),
                new CoordinateModel(103.84685666025423, 1.434671685599842),
                new CoordinateModel(103.87844235361361, 1.4593830407129205),
                new CoordinateModel(103.87088925302767, 1.4882126115142948),
                new CoordinateModel(103.86998011691315, 1.4805007218490491)
            };

            var pointToCheck = new CoordinateModel(103.8727266972518, 1.4708908432922319);

            var isInPolygon = PolygonUtils.IsInPolygon(pointToCheck, polygon);

            Assert.IsTrue(isInPolygon);
        }
    }
}