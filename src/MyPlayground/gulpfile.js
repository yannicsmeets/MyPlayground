/// <binding AfterBuild='build' Clean='clean' ProjectOpened='watch' />

var gulp = require("gulp"),
    del = require('del'),
    cssmin = require('gulp-cssmin'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    jshint = require('gulp-jshint'),
    less = require('gulp-less'),
    concatCss = require('gulp-concat-css'),
    project = require('./project.json');

var directories = {};
directories.webroot = './' + project.webroot + '/';
directories.jsSrc = './Scripts';
directories.jsDest = directories.webroot + 'js';
directories.lessSrc = './Styles';
directories.cssDest = directories.webroot + 'css';

var files = {};
files.js = '**/*.js';
files.minJs = '**/*.min.js';
files.css = '**/*.css';
files.less = '**/*.less';

gulp.task('check:js', function () {
  return gulp.src(directories.jsSrc + files.js)
    .pipe(jshint())
    .pipe(jshint.reporter('default'));
});

gulp.task('clean:js', function () {
  return del(directories.jsDest);
});

gulp.task('build:js', ['check:js', 'clean:js'], function () {
  return gulp.src(directories.jsSrc + files.js, { base: directories.jsSrc })
    .pipe(gulp.dest(directories.jsDest))
    .pipe(uglify())
    .pipe(rename({ extname: '.min.js' }))
    .pipe(gulp.dest(directories.jsDest));
});

gulp.task('check:less', function () {
  // No idea if it is possible to lint less
});

gulp.task('clean:css', function () {
  return del(directories.cssDest);
});

gulp.task('build:css', ['check:less', 'clean:css'], function () {
  return gulp.src(directories.lessSrc + files.less, { base: directories.lessSrc })
    .pipe(less())
    .pipe(gulp.dest(directories.cssDest))
    .pipe(cssmin())
    .pipe(rename({ extname: '.min.css' }))
    .pipe(gulp.dest(directories.cssDest))
    .pipe(concatCss('site.css'))
    .pipe(gulp.dest(directories.cssDest));
});

gulp.task('build', ['build:js', 'build:css']);

gulp.task('clean', ['clean:js', 'clean:css']);

gulp.task('watch', function () {
  gulp.watch(directories.jsSrc + files.js, ['build:js']);
  gulp.watch(directories.lessSrc + files.less, ['build:css']);
});