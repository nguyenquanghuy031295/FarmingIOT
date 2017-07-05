/// <binding AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
var gulp = require('gulp');
var Builder = require("systemjs-builder");
var rimraf = require('rimraf');

var tsc = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');

var paths = {
    npm: "./node_modules/",
    lib: "./wwwroot/lib/",
    img: "./wwwroot/images",
    componentTemplates: "./wwwroot/templates/",
    compiledTS: "./dist/",
    appjs: "./wwwroot/js/",
    bower: "./bower_components/",
    componentCss: "./wwwroot/css/",
    scripts: "./wwwroot/Scripts/",
    bin: "./bin/",
    dist: "./dist/",
    obj: "./obj",
    make: "./make.out",
    nupkg: "./../*.nupkg"
};

var builderOptions = {
    normalize: true,
    runtime: false,
    sourceMaps: false,
    sourceMapContents: false,
    minify: true,
    mangle: false
};
var builder = new Builder('./');
builder.config({
    paths: {
        "n:*": "node_modules/*",
        "rxjs/*": "node_modules/rxjs/*.js",
    },
    map: {
        "rxjs": "n:rxjs",
    },
    packages: {
        "rxjs": { main: "Rx.js", defaultExtension: "js" },
    }
});

gulp.task("build-RxJS-System", function () {
    builder.bundle('rxjs', 'node_modules/.tmp/Rx.umd.min.js', builderOptions).then(function (output) {
        gulp.src("node_modules/.tmp/Rx.umd.min.js").pipe(gulp.dest('wwwroot/lib/rxjs'));
    });

});

var libs = [
    { basePath: paths.npm, relPath: "@angular/common/bundles/common.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/compiler/bundles/compiler.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/core/bundles/core.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/http/bundles/http.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/platform-browser/bundles/platform-browser.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/core/src/util/decorators.js" },
    { basePath: paths.npm, relPath: "@angular/core/src/facade/lang.js" },
    { basePath: paths.npm, relPath: "@angular/router/bundles/router.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/forms/bundles/forms.umd.min.js" },
    { basePath: paths.npm, relPath: "@angular/upgrade/bundles/upgrade.umd.min.js" },

	{ basePath: paths.npm, relPath: "basecode/bundles/basecode.min.js" },
	{ basePath: paths.npm, relPath: "basecode/bundles/basecode.umd.min.js" },
    { basePath: paths.npm, relPath: "angular2-google-maps/core/core.umd.js" },
    { basePath: paths.npm, relPath: "ng2-file-upload/components/file-upload/*.js" },
    { basePath: paths.npm, relPath: "ng2-file-upload/ng2-file-upload.js" },

	{ basePath: paths.npm, relPath: "es6-shim/es6-shim.min.js" },
    { basePath: paths.npm, relPath: "zone.js/dist/zone.js" },
	{ basePath: paths.npm, relPath: "rxjs/bundles/Rx.min.js" },
    { basePath: paths.npm, relPath: "reflect-metadata/reflect.js" },
    { basePath: paths.npm, relPath: "systemjs/dist/system.src.js" },
    { basePath: paths.npm, relPath: "systemjs/dist/system-polyfills.js" },
    { basePath: paths.npm, relPath: "mqtt/dist/mqtt.js"}
];


gulp.task("libs", function () {
    libs.map(function (x) {
        var dest = x.relPath.substr(0, x.relPath.lastIndexOf('/'));
        dest = dest.indexOf('angular') !== -1 ? dest.substr(0, dest.lastIndexOf('/')) : dest;
        gulp.src(x.basePath + x.relPath).pipe(gulp.dest(paths.lib + dest));
    });

    gulp.src(paths.compiledTS + "**/*").pipe(gulp.dest('wwwroot/js/'));
    gulp.src("./scripts/systemjs.config.js").pipe(gulp.dest('wwwroot/js/'));
    gulp.src("./node_modules/.tmp/Rx.umd.min.js").pipe(gulp.dest('wwwroot/lib/rxjs'));
    gulp.src("./external_libs/**/*").pipe(gulp.dest('wwwroot/lib/'));
    gulp.src("./node_modules/moment/moment.js/").pipe(gulp.dest('wwwroot/lib/moment'));
    gulp.src("./node_modules/primeng/components/**/*.js").pipe(gulp.dest(paths.lib + 'primeng/components/'));
    gulp.src("./node_modules/primeng/primeng.js").pipe(gulp.dest(paths.lib + 'primeng/'));

    gulp.src('node_modules/primeng/resources/primeng.min.css').pipe(gulp.dest(paths.componentCss + 'primeng/resources'));
    gulp.src('node_modules/primeng/resources/themes/cupertino/**/*.*').pipe(gulp.dest(paths.componentCss + 'primeng/resources/cupertino'));

    gulp.src('node_modules/font-awesome/css/*.*').pipe(gulp.dest(paths.componentCss + 'font-awesome/css'));
    gulp.src('node_modules/font-awesome/fonts/*.*').pipe(gulp.dest(paths.componentCss + 'font-awesome/fonts'));

    gulp.src("./node_modules/primeui/themes/smoothness/**/*").pipe(gulp.dest('wwwroot/css/primeui/theme/'));
    gulp.src("./node_modules/primeui/primeui-ng-all.min.css").pipe(gulp.dest('wwwroot/css/primeui/'));

    gulp.src("./css/*.css").pipe(gulp.dest('wwwroot/css/'));
});

gulp.task("appCopy", function () {
    gulp.src("scripts/**/*").pipe(gulp.dest("wwwroot/Scripts/"));
});

gulp.task('templates', function () {
    gulp.src('./scripts/**/*.html').pipe(gulp.dest(paths.componentTemplates));
    gulp.src('./scripts/**/*.css').pipe(gulp.dest(paths.componentCss));

});

gulp.task('images', function () {
    gulp.src('./images/**/*.png').pipe(gulp.dest(paths.img));
});

gulp.task('clean', function (callback) {
    var dummyFun = function (x) { };

    rimraf(paths.appjs, dummyFun);
    rimraf(paths.compiledTS, dummyFun);
    rimraf(paths.scripts, dummyFun);
    rimraf(paths.bin, dummyFun);
    rimraf(paths.dist, dummyFun);
    rimraf(paths.obj, dummyFun);
    rimraf(paths.make, dummyFun);
    rimraf(paths.nupkg, dummyFun);

    rimraf(paths.lib, dummyFun);
    rimraf(paths.componentTemplates, dummyFun);
});

gulp.task('compile-css', function (callback) {
        gulp.src('./scripts/**/*.html').pipe(gulp.dest(paths.componentTemplates));
    gulp.src('./scripts/**/*.css').pipe(gulp.dest(paths.componentCss));
});

gulp.task('compile-html', function (callback) {
    gulp.src('./scripts/**/*.html').pipe(gulp.dest(paths.componentTemplates));
});

gulp.task('compile-ts', function (callback) {
    var tsProject = tsc.createProject('tsconfig.json');
    var tsResult = gulp.src('scripts/**/*.ts')
        .pipe(sourcemaps.init())
        .pipe(tsProject());

    gulp.src('scripts/**/*.ts').pipe(gulp.dest(paths.scripts));

    tsResult.js
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(paths.scripts));

});

gulp.task('watch', function (callback) {
    gulp.watch('scripts/**/*.ts', ['compile-ts']);
	gulp.watch('scripts/**/*.html', ['compile-html']);
	gulp.watch(['scripts/**/*.css'], ['compile-css']);
	gulp.watch('responsive-sass/**/*.scss', function() {
        gulp.src("./scripts/systemjs.config.js").pipe(gulp.dest('wwwroot/js/'));
    });
});

gulp.task('default', ['build-RxJS-System', 'libs', 'templates', 'appCopy', 'images'], function () {
    // nothing
});
