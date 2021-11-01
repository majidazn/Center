var model;
var Dashboard = {

  settings: {
    columns: '.column',
    widgetSelector: '.widget',
    handleSelector: '.widget-head',
    contentSelector: '.widget-content',
    widgetDefault: {
      movable: true,
      removable: true,
      collapsible: true
    },
    widgetIndividual: {
      divID: {
        movable: false,
        removable: false,
        collapsible: false
      }
    }
  },

  init: function (dashboardModel) {
    model = dashboardModel;
    this.addWidgetControls();
    this.makeSortable();
  },

  getWidgetSettings: function (id) {
    var settings = this.settings;
    return (id && settings.widgetIndividual[id]) ? $.extend({}, settings.widgetDefault, settings.widgetIndividual[id]) : settings.widgetDefault;
  },
  deleteDashboard: function (dashboardId) {
    openModalConfirmation(400, 0, 'آیا از حذف داشبورد اطمینان دارید؟', 'حذف', 'انصراف', "deleteDashboardAftterConfrim('" + dashboardId + "')", null);
  },
  addWidgetControls: function () {
    var Dashboard = this, settings = this.settings;

    $(settings.widgetSelector, $(settings.columns)).each(function () {
      var thisWidgetSettings = Dashboard.getWidgetSettings(this.id);
      var thisWidgetID = this.id;
      if ($('#' + this.id + ' div a').is('.remove') == false) {
        if (thisWidgetSettings.removable) {
          $('<a class="remove" style="cursor:pointer"><i class="fa fa-times" aria-hidden="true"></i></a>')
            .mousedown(function (e) {
              e.stopPropagation();
              e.preventDefault();
            })
            .click(function () {
              
              var isConfirm = confirm('آیا از حذف پنل مورد نظر اطمینان دارید؟', 'حذف', 'انصراف');
              if (isConfirm)
                deletWidget(thisWidgetID);
              //openModalConfirmation(400, 0, 'آیا از حذف پنل مورد نظر اطمینان دارید؟', 'حذف', 'انصراف', "deletWidget('" + thisWidgetID + "')", null);

            }).appendTo($(settings.handleSelector, this));
        }

        $('<a  class="edit" style="cursor:pointer"><i class="fa fa-cog" aria-hidden="true"></i></a>').mousedown(function (e) {
          e.stopPropagation();
          e.preventDefault();
        }).click(function () {
          var dashboardwidgetid = $('#' + thisWidgetID).data("dashboardwidgetid");
          openModal('/Widget/WidgetEditor/' + dashboardwidgetid, '80%', '520', $('#' + thisWidgetID).find('span').html());
        }).appendTo($(settings.handleSelector, this));

        $('<a class="expand" style="cursor:pointer"><i class="fa fa-expand" aria-hidden="true"></i></a>').mousedown(function (e) {
          e.stopPropagation();
          e.preventDefault();
        }).click(function () {
          
          if ($(this.parentElement.parentElement).hasClass("expanded-widget")) {
            $(this.parentElement.parentElement).removeClass("expanded-widget");
            $(this.parentElement.parentElement).removeClass('fullscreenClass');
            $(this).closest('.widget-head-expanded').removeClass('widget-head-expanded').addClass('widget-head')
            $(this.parentElement.parentElement).find(".edit").show();
            $(this.parentElement.parentElement).find(".remove").show();
            $(this.parentElement.parentElement).find(".collapse").show();
            this.firstElementChild.className = "fa fa-expand";
            $(".navbar-default").css("z-index", 999);
            $(".footer").css("z-index", 999);
          }
          else {
            $(this.parentElement.parentElement).addClass("expanded-widget");
            $(this).closest('.widget-head').removeClass('widget-head').addClass('widget-head-expanded');
            $(this.parentElement.parentElement).find(".edit").hide();
            $(this.parentElement.parentElement).find(".remove").hide();
            $(this.parentElement.parentElement).find(".collapse").hide();
            this.firstElementChild.className = "fa fa-compress";
            $(".navbar-default").css("z-index", 0);
            $(".footer").css("z-index", 0);
            $(this.parentElement.parentElement).addClass('fullscreenClass');
          }
          //Dashboard.configureWidget(thisWidgetID.replace('dw', ''));
          return false;
        }).appendTo($(settings.handleSelector, this));

        if (thisWidgetSettings.collapsible) {
          $('<a  class="collapse" style="cursor:pointer"><i class="fa fa-caret-up" aria-hidden="true"></i></a>').mousedown(function (e) {
            e.stopPropagation();
            e.preventDefault();
          }).click(function () {
            $(this).parents(settings.widgetSelector).toggleClass('collapsed');
            var iframeID = 'iframe' + $(this).parent().parent().attr('id');
            if (!$(this).parents(settings.widgetSelector).hasClass('collapsed')) {
              $(this.firstElementChild).removeClass("fa-caret-down");
              $(this.firstElementChild).addClass("fa-caret-up");
              $('#' + iframeID).attr('src', $('#' + iframeID).attr('src'));
            }
            else {
              $(this.firstElementChild).removeClass("fa-caret-up");
              $(this.firstElementChild).addClass("fa-caret-down");
              $('#' + iframeID).contents().find("div.container-fluid").html('');
            }
            Dashboard.savePreferences();
            return false;
          }).prependTo($(settings.handleSelector, this));
        }
      }
    });
  },

  attachStylesheet: function (href) {
    return $('<link href="' + href + '" rel="stylesheet" type="text/css" />').appendTo('head');
  },

  makeSortable: function () {
    var Dashboard = this,
      settings = this.settings,
      $sortableItems = (function () {
        var notSortable = '';
        $(settings.widgetSelector, $(settings.columns)).each(function (i) {
          if (!Dashboard.getWidgetSettings(this.id).movable) {
            if (!this.id) {
              this.id = 'widget-no-id-' + i;
            }
            notSortable += '#' + this.id + ',';
          }
        });
        if (notSortable.length == 0)
          return $('> li', settings.columns);
        else
          return $('> li:not(' + notSortable + ')', settings.columns);
      })();

    $sortableItems.find(settings.handleSelector).css({
      cursor: 'move'
    }).mousedown(function (e) {

      //if (!e.target.parentElement.classList.contains("expanded-widget")) {
      //    $sortableItems.css({ width: '' });
      //    $(this).parent().css({
      //        width: $(this).parent().width() + 'px'
      //    });
      //}

    }).mouseup(function (e) {
      //
      //e.stopPropagation();
      //e.preventDefault();
      //if (!$(this).parent().hasClass('dragging')) {
      //    $(this).parent().css({ width: '' });
      //} else {
      //    $(settings.columns).sortable('disable');
      //}
    });

    $(settings.columns).sortable({
      items: $sortableItems,
      connectWith: $(settings.columns),
      handle: settings.handleSelector,
      placeholder: 'widget-placeholder',
      forcePlaceholderSize: true,
      revert: 300,
      delay: 100,
      opacity: 0.8,
      containment: 'document',
      start: function (e, ui) {
        $(ui.helper).addClass('dragging');
      },
      stop: function (e, ui) {
        $(ui.item).css({ width: '' }).removeClass('dragging');
        $(settings.columns).sortable('enable');
        var widgetId = ui.item[0].id;
        var orderNum = ui.item.index();
        var colNum = ui.item.parent().index();
        var rowNum = (ui.item.parent().parent().index());
        var objWidget = model.DashboardWidget.find(function (x) {
          // Filter out the appropriate one
          return x.Id == $("#" + widgetId).data("dashboardwidgetid");
        });//find(it=>it.Id== $("#" + widgetId).data("dashboardwidgetid"));
        objWidget.WidgetRowNum = rowNum;
        objWidget.WidgetColNum = colNum;
        objWidget.WidgetOrderNum = orderNum;

        var url = "/Dashboard/UpdateDashboardWidget";
        url = BaseUrl() + url;
        url = url.replace('//', '/');

        $.ajax({
          type: "post",
          data: {
            objWidget: objWidget
          },
          url: url,
          success: function (data) {
            Dashboard.savePreferences();
          }
        });
      }
    });
  },

  savePreferences: function () {
    var Dashboard = this,
      settings = this.settings,
      paramString = '';

    var columnPos = 0;

    $(settings.columns).each(function (i) {

      var sortOrder = 0;

      $(settings.widgetSelector, this).each(function (i) {
        var kpiName = $(this).attr('id');
        var kpiTitle = $(this).find('span').text();
        var kpiUrl = $(this).find('iframe').attr('src');
        var kpiColumnPos = columnPos;
        var kpiSortOrder = sortOrder++;
        var kpiExpanded = $(settings.contentSelector, this).css('display') === 'none' ? '0' : '1';

        paramString += kpiName + ':';
        paramString += kpiTitle + '|' + kpiUrl + '|' + kpiColumnPos + '|' + kpiSortOrder + '|' + kpiExpanded;
        paramString += ';';
      });

      columnPos++;

    });

    //save widget settings 
    //alert(paramString);
  },
  addDashboard: function () {

    $('<div title="افزودن داشبورد" id="dialog_add_widget" style="overflow:hidden"><iframe src="Dashboard/AddWidget" style="border:0px;width:80%;height:98%;" frameborder="0"></iframe></div>').dialog({
      resizable: true,
      width: 412,
      height: 375,
      modal: true,
      close: function (ev, ui) { $(this).remove(); }
    });

    $('#dialog_add_widget iframe').load(function () {
      $("#dialog_add_widget").dialog("option", "height", this.contentWindow.document.body.offsetHeight + 100);
    });

    return false;
  },
  addWidget: function () {

    $('<div title="افزودن داشبورد" id="dialog_add_widget" style="overflow:hidden"><iframe src="' + addWidgetUrl + '" style="border:0px;width:80%;height:98%;" frameborder="0"></iframe></div>').dialog({
      resizable: true,
      width: 1100,
      height: 500,
      modal: true,
      close: function (ev, ui) { $(this).remove(); }
    });

    $('#dialog_add_widget iframe').load(function () {
      $("#dialog_add_widget").dialog("option", "height", this.contentWindow.document.body.offsetHeight + 100);
    });

    return false;
  },
  setWidgetData: function (widgetElement, dataObject) {

    $(widgetElement).attr("data-DashboardId", dataObject.DashboardId);
    $(widgetElement).attr("data-DashboardWidgetId", dataObject.WidgetId);
    $(widgetElement).attr("data-WidgetId", dataObject.WidgetId);
    $(widgetElement).attr("data-WidgetColNum", dataObject.WidgetColNum);
    $(widgetElement).attr("data-WidgetRowNum", dataObject.WidgetRowNum);
    $(widgetElement).attr("data-WidgetOrderNum", dataObject.WidgetOrderNum);
  },
  addWidgetToDashboard: function (kpiTitle, kpiName, row, column, objDashboardWidget,index) {
    //TODO: add widget
    var id = kpiName + index;
    var data =
      '<li id="' + id + '" class="widget"><div class="widget-head"><span>'
      + kpiTitle + '</span></div><div class="widget-content"></div></li>';

    if (window.parent.$('.container-fluid .row-fluid').length > 0) {
      if (row != undefined && row != null && column != undefined && column != null) {
        var div = document.createElement('div');
        div.innerHTML = data;
        Dashboard.setWidgetData(div.firstChild, objDashboardWidget);
        window.parent.$('.container-fluid .row-fluid')[row].children[column].appendChild(div.firstChild);
      }
      else {
        window.parent.$('.container-fluid .row-fluid ul:first').append(data);
      }
    }
    else {
      window.parent.$('.container-fluid').html('<div ><ul class="column">' + data + '</ul><ul class="column"></ul><ul class="column"></ul>');
    }

    window.parent.Dashboard.savePreferences();
    window.parent.Dashboard.addWidgetControls();
    window.parent.Dashboard.makeSortable();

    window.parent.$('#dialog_add_widget').remove();

    return false;
  },

  configureWidget: function (dwid) {

    $('<div title="Configure Widget" id="dialog_configure_widget" style="overflow:hidden"><iframe src="widgets/config.html" style="border:0px;width:100%;height:98%;" frameborder="0"></iframe></div>').dialog({
      resizable: false,
      width: 550,
      modal: true,
      buttons: {
        "Save Widget": function () {
          $(this).dialog("close");
        },
        Cancel: function () {
          $(this).dialog("close");
        }
      }
    });

    $('#dialog_configure_widget iframe').load(function () {
      $("#dialog_configure_widget").dialog("option", "height", this.contentWindow.document.body.offsetHeight + 150);
    });

    return false;
  }

};

function deleteDashboardAftterConfrim(dashboardId) {
  var url = "/Dashboard/DeleteDashboard";
  url = BaseUrl() + url;
  url = url.replace('//', '/');
  $.ajax({
    type: "post",
    data: {
      dashboardId: dashboardId
    },
    url: url,
    success: function (data) {
      window.location.replace("/Home/Index");
    }
  });
}

function BaseUrl() {

  return "http://192.168.5.89/api/";
}
function deletWidget(thisWidgetID) {
  $('#' + thisWidgetID).animate({
    opacity: 0
  }, function () {
    $('#' + thisWidgetID).wrap('<div/>').parent().slideUp(function () {
      var dashboardwidgetid = $('#' + thisWidgetID).data("dashboardwidgetid");
      var dashboardid = $('#' + thisWidgetID).data("dashboardid");

      var url = "Dashboard/DeleteDashboardWidget";
      url = BaseUrl() + url;
      url = url.replace('//', '/');

      $.ajax({
        type: "Get",
        data: {
          dashboardWidgetId: dashboardwidgetid,
          dashboardId: dashboardid
        },
        url: BaseUrl()+"Dashboard/DeleteDashboardWidget",
        crossDomain: true,
        success: function (data) {
          $('#' + thisWidgetID).remove();
          $('#confirmModal').modal('hide');
        }
      });
    });
  });
}


