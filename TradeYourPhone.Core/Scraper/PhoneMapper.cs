using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Scraper
{
    public static class PhoneMapper
    {
        public static List<MappedPhone> MonsterMobileMapping = new List<MappedPhone>()
        {
            new MappedPhone() { title="Samsung Galaxy S4 16gb", link = "/samsung/galaxy-s4-i9500-16gb", PhoneModelId = 1 },
            new MappedPhone() { title="Samsung Galaxy S5 16gb", link = "/samsung/galaxy-s5-g900-16gb", PhoneModelId = 2 },
            new MappedPhone() { title="Samsung Galaxy S6 32gb", link = "/samsung/galaxy-s6-g920-32gb", PhoneModelId = 3 },
            new MappedPhone() { title="Sony Xperia Z3", link = "/sony/xperia-z3", PhoneModelId = 4 },
            new MappedPhone() { title="iPhone 5 16gb", link = "/apple/iphone-5-16gb", PhoneModelId = 5 },
            new MappedPhone() { title="iPhone 5s 16gb", link = "/apple/iphone-5s-16gb", PhoneModelId = 6 },
            new MappedPhone() { title="iPhone 6 16gb", link = "/apple/iphone-6-16gb", PhoneModelId = 7 },
            new MappedPhone() { title="iPhone 6 Plus 16gb", link = "/apple/iphone-6-plus-16gb", PhoneModelId = 8 },
            new MappedPhone() { title="Sony Xperia Z2", link = "/sony/xperia-z2", PhoneModelId = 10 },
            new MappedPhone() { title="iPhone 4 32gb", link = "/apple/iphone-4-32gb", PhoneModelId = 11 },
            new MappedPhone() { title="HTC One M8", link = "/htc/one-m8-16gb", PhoneModelId = 12 },
            new MappedPhone() { title="iPhone 4s 16gb", link = "/apple/iphone-4s-16gb", PhoneModelId = 13 },
            new MappedPhone() { title="Galaxy S4 (32GB)", link = "/samsung/galaxy-s4-i9505-32gb", PhoneModelId = 14 },
            new MappedPhone() { title="Galaxy S5 (32GB)", link = "/samsung/galaxy-s5-g900-32gb", PhoneModelId = 15 },
            new MappedPhone() { title="Galaxy S6 Edge (32GB)", link = "/samsung/galaxy-s6-edge-32gb", PhoneModelId = 16 },
            new MappedPhone() { title="Galaxy S6 (64GB)", link = "/samsung/galaxy-s6-g920-64gb", PhoneModelId = 17 },
            new MappedPhone() { title="Galaxy S6 Edge (64GB)", link = "/samsung/galaxy-s6-edge-64gb", PhoneModelId = 18 },
            new MappedPhone() { title="Galaxy S3 (16GB)", link = "/samsung/galaxy-s3-i9300-16gb", PhoneModelId = 19 },
            new MappedPhone() { title="Galaxy S3 (32GB)", link = "/samsung/galaxy-s3-i9300-32gb", PhoneModelId = 20 },
            new MappedPhone() { title="iPhone 4 (16GB)", link = "/apple/iphone-4-16gb", PhoneModelId = 21 },
            new MappedPhone() { title="iPhone 4S (32GB)", link = "/apple/iphone-4s-32gb", PhoneModelId = 22 },
            new MappedPhone() { title="iPhone 5 (32GB)", link = "/apple/iphone-5-32gb", PhoneModelId = 23 },
            new MappedPhone() { title="iPhone 5S (32GB)", link = "/apple/iphone-5s-32gb", PhoneModelId = 24 },
            new MappedPhone() { title="iPhone 5S (64GB)", link = "/apple/iphone-5s-64gb", PhoneModelId = 25 },
            new MappedPhone() { title="iPhone 5C (16GB)", link = "/apple/iphone-5c-16gb", PhoneModelId = 26 },
            new MappedPhone() { title="iPhone 5C (32GB)", link = "/apple/iphone-5c-32gb", PhoneModelId = 27 },
            new MappedPhone() { title="iPhone 5C (8GB)", link = "/apple/iphone-5c-8gb", PhoneModelId = 28 },
            new MappedPhone() { title="iPhone 6 (64GB)", link = "/apple/iphone-6-64gb", PhoneModelId = 29 },
            new MappedPhone() { title="iPhone 6 (128GB)", link = "/apple/iphone-6-128gb", PhoneModelId = 30 },
            new MappedPhone() { title="iPhone 6 Plus (64GB)", link = "/apple/iphone-6-plus-64gb", PhoneModelId = 31 },
            new MappedPhone() { title="iPhone 6 Plus (128GB)", link = "/apple/iphone-6-plus-128gb", PhoneModelId = 32 },
            new MappedPhone() { title="Xperia Z3 Compact", link = "/sony/xperia-z3-compact", PhoneModelId = 33 },
            new MappedPhone() { title="One M8 (32GB)", link = "/htc/one-m8-32gb", PhoneModelId = 34 },
            new MappedPhone() { title="One M9 (32GB)", link = "/htc/one-m9-32gb", PhoneModelId = 35 },
            new MappedPhone() { title="Galaxy Note 2 4G", link = "/samsung/galaxy-note-2-n7105-4g", PhoneModelId = 36 },
            new MappedPhone() { title="Galaxy Note 3 (16GB)", link = "/samsung/galaxy-note-3-n9005-4g-16gb", PhoneModelId = 37 },
            new MappedPhone() { title="Galaxy Note 3 (32GB)", link = "/samsung/galaxy-note-3-n9005-4g-32gb", PhoneModelId = 38 },
            new MappedPhone() { title="Galaxy Note 3 (64GB)", link = "/samsung/galaxy-note-3-n9005-4g-64gb", PhoneModelId = 39 },
            new MappedPhone() { title="Galaxy Note 4 (32GB)", link = "/samsung/galaxy-note-4-32gb", PhoneModelId = 40 },
            new MappedPhone() { title="Lumia 640 XL", link = "/nokia/lumia-640-xl", PhoneModelId = 41 },
            new MappedPhone() { title="Nokia 930", link = "/nokia/lumia-930", PhoneModelId = 42 },
            new MappedPhone() { title="Lumia 1020 (32GB)", link = "/nokia/lumia-1020-32gb", PhoneModelId = 43 },
            new MappedPhone() { title="LG G3 (16GB)", link = "/lg/g3-16gb", PhoneModelId = 44 },
            new MappedPhone() { title="LG G3 (32GB)", link = "/lg/g3-32gb", PhoneModelId = 45 },
            new MappedPhone() { title="LG G2 (16GB)", link = "/lg/g2-16gb", PhoneModelId = 46 },
            new MappedPhone() { title="LG G2 (32GB)", link = "/lg/g2-32gb", PhoneModelId = 47 },
            new MappedPhone() { title="Google Nexus 4", link = "/google/nexus-4", PhoneModelId = 48 },
            new MappedPhone() { title="Google Nexus 5 (16GB)", link = "/google/nexus-5-16gb", PhoneModelId = 49 },
            new MappedPhone() { title="Google Nexus 5 (32GB)", link = "/google/nexus-5-32gb", PhoneModelId = 50 },
            new MappedPhone() { title="Google Nexus 6 (32GB)", link = "/motorola/nexus-6-xt110-32gb", PhoneModelId = 51 }
        };

        public class MappedPhone
        {
            public string title { get; set; }
            public string link { get; set; }
            public int PhoneModelId { get; set; }
        }
    }
}
