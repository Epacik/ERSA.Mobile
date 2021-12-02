using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERSA.Mobile.Licenses
{
    internal static class Licenses
    {
        public static IEnumerable<License> GetAll() => new License[]{
            ("Microsoft.NETCore.UniversalWindowsPlatform", 
                ThirdParty.MicrosoftNETCoreUniversalWindowsPlatform.License, 
                "https://github.com/Microsoft/dotnet/blob/master/releases/UWP/LICENSE.TXT"),

            ("NETStandard.Library", 
                ThirdParty.NETStandardLibrary.License, 
                "https://github.com/dotnet/standard/blob/master/LICENSE.TXT"),

            ("Newtonsoft.Json", 
                ThirdParty.NewtonsoftJson.License, 
                "https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md"),

            ("RestSharp, RestSharp.Serializers.NewtonsoftJson",
                ThirdParty.RestSharp.License,
                "https://github.com/restsharp/RestSharp/blob/dev/LICENSE.txt"),

            ("Serilog, Serilog.Sinks.Console, Serilog.Sinks.Xamarin",
                ThirdParty.Serilog.License,
                "https://github.com/serilog/serilog/blob/dev/LICENSE"),

            ("System.Numeric.Vectors",
                ThirdParty.SystemNumericVectors.License,
                "https://github.com/dotnet/corefx/blob/master/LICENSE.TXT"),

            ("Xamarin.Community.Toolkit",
                ThirdParty.XamarinCommunityToolkit.License,
                "https://github.com/xamarin/XamarinCommunityToolkit/blob/main/LICENSE"),

            ("Xamarin.Essentials", 
                ThirdParty.XamarinEssentials.License,
                "https://github.com/xamarin/Essentials/blob/main/LICENSE"),

            ("Xamarin.Forms",
                ThirdParty.XamarinForms.License,
                "https://github.com/xamarin/Xamarin.Forms/blob/5.0.0/LICENSE"),
        }.OrderBy(x => x.license);
    }

    public struct License
    {
        public string library;
        public string license;
        public string link;

        public License(string library, string license, string link)
        {
            this.library = library;
            this.license = license;
            this.link = link;
        }

        public override bool Equals(object obj)
        {
            return obj is License other &&
                   library == other.library &&
                   license == other.license &&
                   link == other.link;
        }

        public override int GetHashCode()
        {
            int hashCode = -660377315;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(library);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(license);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(link);
            return hashCode;
        }

        public void Deconstruct(out string library, out string license, out string link)
        {
            library = this.library;
            license = this.license;
            link = this.link;
        }

        public static implicit operator (string library, string license, string link)(License value)
        {
            return (value.library, value.license, value.link);
        }

        public static implicit operator License((string library, string license, string link) value)
        {
            return new License(value.library, value.license, value.link);
        }
    }
}
