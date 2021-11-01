import { Component, OnInit, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { AppComponent } from '../../../app.component';
declare var jquery: any;
declare var $: any;

@Component({
  selector: 'app-center-layout',
  templateUrl: './center-layout.component.html',
  styleUrls: ['./center-layout.component.scss'],
})
  
/** Center-Layout component*/
export class CenterLayoutComponent implements OnInit, AfterViewInit {
  isAuthenticated = true; 
  breadCrubmbTitle: string;
  /** Center-Layout ctor */

  constructor(
    private router: Router, private route: ActivatedRoute, private appComponent: AppComponent) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.addBreadCrumbTitle();
      this.addValidatorClass();
       
    });

  }  

  ngOnInit() {
    this.appComponent.setTitle("مراکز"); 
    this.router.events.subscribe((val) => {
      this.collapsable();
      this.onready();
      this.addValidatorClass();
    });
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {

      this.addBreadCrumbTitle();
      this.addValidatorClass();      

      });
    var name = localStorage.getItem("pattern-theme")
    if (name) {
      
      $('body').removeClass(function (index, css) {
        return (css.match(/\btheme\S+/g) || []).join(' '); // removes anything that starts with "theme"
      });
      $('body').addClass(name);
    }
    var colorname = localStorage.getItem("pattern-color")
    if (colorname) {
      
      $('body').removeClass(function (index, css) {
        return (css.match(/\bcolor\S+/g) || []).join(' '); // removes anything that starts with "color"
      });
      $('body').addClass(colorname);
    }
  }

  ngAfterViewInit() {

    this.collapsable();
    this.onready();
    this.themeToggle();

    $(".ribbon-section").parents("#page-content-wrapper").addClass("has-ribbonTop");
  }



  public collapsable() {
    var url = this.GetUrl();

    if (url != "/") {
      $('#sidebar-wrapper').find('.list-group-item').removeClass("active-link");
      $('#sidebar-wrapper').find('a[href="' + url + '"]').addClass("active-link");
      $(".active-link").parent().addClass("show");
      $(".active-link").parent().parent().addClass("show");
      $(".active-link").parent().prev(".list-group-item.item-level2").removeClass("collapsed");
      $(".active-link").parent().prev(".list-group-item.item-level2").attr('aria-expanded', 'true');
      $(".active-link").parent().parent().prev(".list-group-item.item-level1").removeClass("collapsed");
      $(".active-link").parent().parent().prev(".list-group-item.item-level1").attr('aria-expanded', 'true');
    }
  }

  public GetUrl() {
    var url = window.location.href;
    url = url.replace(/^.*\/\/[^\/]+/, '');
    url = url.toLowerCase();

    var urlLength = url.length;

    if (url.substring(urlLength - 6, urlLength) === '/index')
      url = url.substring(0, urlLength - 6);

    if (url.substring(urlLength - 1, urlLength) === '#') {
      url = url.substring(0, urlLength - 1);
    }
    if (url.substring(urlLength - 1, urlLength) === '/') {
      url = url.substring(0, urlLength - 1);
    }
    return url;
  }

  //theme settings
  public themeToggle() {
    $(".theme-collapse-btn").click(function () {
      $(".theme-panel").toggleClass("expand");
    });
  }

  public addValidatorClass() {
    $.each($("input"), function (i, input) {
      if ($(input).next("app-control-messages").length > 0) {
        $(input).addClass("hasValidator");
      }
    });
    $.each($("kendo-combobox"), function (i, combobox) {
      if ($(combobox).next("app-control-messages").length > 0) {
        $(combobox).children().children().children("input").addClass("hasValidator");
      }
    });
  }
  public addBreadCrumbTitle() {
     
    let str = this.router.url.replace(new RegExp("\/[0-9]+", "g"), "")
    let index = str.lastIndexOf('/');
    let path = str.substr(index + 1);

      if (path.length == 0)
        return;

    var snapshot: any = this.route.snapshot;
    //for home page return route config
    if (index == 0) {
      var currentRouteConfig = snapshot.routeConfig.children.find(function (element) {
        if (element.data.title) {
          return element;
        }
      });
    }
    //for other page return route config
    else {
      var currentRouteConfig = snapshot.routeConfig.children.find(function (element) {
        if (element.path.startsWith(path)) {
          return element;
        }
      });
    }
    //add bradcrumb title
    if (currentRouteConfig) {
      this.breadCrubmbTitle = currentRouteConfig.data.breadcrumb;
    }
    //add hasRibbon class if page has ribbon
      if (currentRouteConfig  && currentRouteConfig.data.hasRibbon) {
        $("#page-content-wrapper").addClass("has-ribbonTop");
      }
      else {

        $("#page-content-wrapper").removeClass("has-ribbonTop");
      }
  }
  public onready() {

    //sidebar hide when click link-item.... 
    $("#sidebar-wrapper .list-group .link-item").click(function () {
      $("#wrapper").removeClass("toggled");
    });

    $(".tab-section .btn").click(function () {
      $(".tab-section .btn").removeClass("active");
      $(this).addClass("active");
    });

    $(".k-splitter .k-scrollable").addClass("scroll-style");


    //pattern settings
    $(".theme-panel .theme-list.pattern > li span").click(function () {
      var name = $(this).attr("name");
      $('body').removeClass(function (index, css) {
        return (css.match(/\btheme\S+/g) || []).join(' '); // removes anything that starts with "theme"
      });
      $('body').addClass(name);
      localStorage.setItem("pattern-theme", name);

    });

    //color settings
    $(".theme-panel .theme-list.color > li span").click(function () {
      var name = $(this).attr("name");
      $('body').removeClass(function (index, css) {
        return (css.match(/\bcolor\S+/g) || []).join(' '); // removes anything that starts with "color"
      });
      $('body').addClass(name);
      localStorage.setItem("pattern-color", name);

    });

    //click outside theme-collapse-btn, close theme-panel
    $(document).click(function (e) {
      if (!$(e.target).is('.theme-collapse-btn , .theme-collapse-btn img')) {
        $('.theme-panel').removeClass("expand");
      }
    });
  }
}


