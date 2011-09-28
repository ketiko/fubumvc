﻿$(document).ready(function () {

    var resetDottedLines = function () {
        if ($(this).find('.children:visible').size() != 0) {
            $(this).addClass('with-children');
        }
        else {
            $(this).removeClass('with-children');
        }
    };

    $('#nodes').treeview({
        collapsed: true,
        animated: 'fast',
        prerendered: true,
        toggle: function () {
            resetDottedLines.call(this);
        }
    });

    $('#nodes > li').each(function () {
        resetDottedLines.call(this);
    });

    $('.console > .message > ul').each(function () {
        $(this).find('li.binding-detail:last').each(function () {
            $(this).addClass('last');
        });
    });

    $('.output').each(function () {
        var metadata = $(this).metadata();
        if (metadata.type && metadata.type == 'application/json') {
            var self = $(this);
            var json = eval('(' + self.find('code').html() + ')');
            self.html('');
            self.append(prettyPrint(json));
        }
    });

    var logViewer = $('#log-viewer');
    var requestViewer = $('#request-viewer');
    var viewLogs = $('#ViewLogs');
    var viewRequests = $('#ViewRequests');

    viewLogs.click(function () {
        requestViewer.hide();
        logViewer.show();
        viewRequests.show();
        viewLogs.hide();
    });

    viewRequests.click(function () {
        logViewer.hide();
        requestViewer.show();
        viewLogs.show();
        viewRequests.hide();
    });

    var arrow = $('#chain-arrow').html();
    $('.chain-visualizer > li:not(:last)').after('<li class="arrow">' + arrow + '</li>');
});