//$(document).ready(function () {
 

//  collapsable();

//  $(".hamburger").click(function () {
//    $("main").toggleClass("full-width");

//  });


//  if ($(window).width() < 767) {
//    $("#sidebar").removeClass("show");
//  }

//  $(".tab-section .btn").click(function () {
//    $(this).addClass("active");
//  });
//});



//function collapsable() {

//  var url = GetUrl();
//  
//  if (url != "/") {
//    $('#sidebar').find('.list-group-item').removeClass("active-link");
//    $('#sidebar').find('a[href="' + url + '"]').addClass("active-link");
//    $(".active-link").parent().addClass("show");
//    $(".active-link").parent().parent().addClass("show");
//    $(".active-link").parent().prev(".list-group-item.item-level2").removeClass("collapsed");
//    $(".active-link").parent().prev(".list-group-item.item-level2").attr('aria-expanded', 'true');
//    $(".active-link").parent().parent().prev(".list-group-item.item-level1").removeClass("collapsed");
//    $(".active-link").parent().parent().prev(".list-group-item.item-level1").attr('aria-expanded', 'true');

//  }
//}

//$(document).on('click', '.link-item', function () {
//  HighLightSelectedMenu();
//});

//function GetUrl() {
//  var url = window.location.href;
//  url = url.replace(/^.*\/\/[^\/]+/, '');
//  url = url.toLowerCase();

//  var urlLength = url.length;

//  if (url.substring(urlLength - 6, urlLength) === '/index')
//    url = url.substring(0, urlLength - 6);

//  if (url.substring(urlLength - 1, urlLength) === '#') {
//    url = url.substring(0, urlLength - 1);
//  }
//  if (url.substring(urlLength - 1, urlLength) === '/') {
//    url = url.substring(0, urlLength - 1);
//  }
//  return url;
//}
//function HighLightSelectedMenu() {
//  var url = GetUrl();
//  console.log('aaaaa');
//  if (url != "/") {
//    $('#sidebar').find('.list-group-item').removeClass("active-link");
//    $('#sidebar').find('a[href="' + url + '"]').addClass("active-link");
//    $(".active-link").parent().addClass("show");
//    $(".active-link").parent().parent().addClass("show");
//    $(".active-link").parent().prev(".list-group-item.item-level2").removeClass("collapsed");
//    $(".active-link").parent().prev(".list-group-item.item-level2").attr('aria-expanded', 'true');
//    $(".active-link").parent().parent().prev(".list-group-item.item-level1").removeClass("collapsed");
//    $(".active-link").parent().parent().prev(".list-group-item.item-level1").attr('aria-expanded', 'true');
//  }
//}
