using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace TradeYourPhone.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var cssBundle = new StyleBundle("~/bundles/css");
            cssBundle.Include("~/Content/Site.less", "~/Content/bootstrap/bootstrap.less");
            cssBundle.Transforms.Add(cssTransformer);
            cssBundle.Orderer = nullOrderer;
            bundles.Add(cssBundle);

            var adminCSSBundle = new StyleBundle("~/bundles/adminCss");
            adminCSSBundle.Include("~/Content/AdminSite.less", "~/Content/bootstrap/bootstrap.less");
            adminCSSBundle.Transforms.Add(cssTransformer);
            adminCSSBundle.Orderer = nullOrderer;
            bundles.Add(adminCSSBundle);

            var jqueryBundle = new ScriptBundle("~/bundles/jquery");
            jqueryBundle.Include("~/Scripts/Libraries/jquery-{version}.js");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            jqueryvalBundle.Include("~/Scripts/Libraries/jquery.validate*");
            jqueryvalBundle.Transforms.Add(jsTransformer);
            jqueryvalBundle.Orderer = nullOrderer;
            bundles.Add(jqueryvalBundle);

            var externalLibs = new ScriptBundle("~/bundles/externalLibs");
            externalLibs.Include("~/Scripts/lodash.min.js");
            externalLibs.Transforms.Add(jsTransformer);
            externalLibs.Orderer = nullOrderer;
            bundles.Add(externalLibs);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            var modernizrBundle = new ScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/Scripts/Libraries/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);

            var bootstrapBundle = new ScriptBundle("~/bundles/bootstrap");
            bootstrapBundle.Include("~/Scripts/Libraries/bootstrap.js", "~/Scripts/respond.js");
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            var angularBundle = new ScriptBundle("~/bundles/angular");
            angularBundle.Include("~/Scripts/Libraries/Angular/angular.min.js");
            angularBundle.Include("~/Scripts/Libraries/Angular/angular-route.js");
            angularBundle.Include("~/Scripts/Libraries/Angular/angular-cookies.js");
            angularBundle.Transforms.Add(jsTransformer);
            angularBundle.Orderer = nullOrderer;
            bundles.Add(angularBundle);

            var angularUIBootstrapBundle = new ScriptBundle("~/bundles/angularUIBootstrap");
            angularUIBootstrapBundle.Include("~/Scripts/angular-ui/ui-bootstrap.min.js");
            angularUIBootstrapBundle.Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js");
            angularUIBootstrapBundle.Transforms.Add(jsTransformer);
            angularUIBootstrapBundle.Orderer = nullOrderer;
            bundles.Add(angularUIBootstrapBundle);

            var angularModulesBundle = new ScriptBundle("~/bundles/angularModules");
            angularModulesBundle.Include("~/Scripts/Libraries/Angulartics/angulartics.min.js", "~/Scripts/Libraries/Angulartics/angulartics-ga.min.js", "~/Scripts/Libraries/Angular/angular-mocks.js");
            angularModulesBundle.Transforms.Add(jsTransformer);
            angularModulesBundle.Orderer = nullOrderer;
            bundles.Add(angularModulesBundle);

            var angularticsBundle = new ScriptBundle("~/bundles/angulartics");
            angularticsBundle.Include("~/Scripts/Libraries/Angulartics/angulartics.min.js");
            angularticsBundle.Include("~/Scripts/Libraries/Angulartics/angulartics-ga.min.js");
            angularticsBundle.Transforms.Add(jsTransformer);
            angularticsBundle.Orderer = nullOrderer;
            bundles.Add(angularticsBundle);

            var angularApp = new ScriptBundle("~/bundles/angularApp");
            angularApp.IncludeDirectory("~/AngularApp", "*.js", true);
            angularApp.Transforms.Add(jsTransformer);
            angularApp.Orderer = nullOrderer;
            bundles.Add(angularApp);
        }
    }
}