/*
Name: 			UI Elements / Charts - Examples
Written by: 	Okler Themes - (http://www.okler.net)
Theme Version: 	1.4.1
*/

(function ($) {

    'use strict';




    var YearMonth = $("#ContentPlaceHolder1_hfYearMonth").val();
    var YearMonthValue = $("#ContentPlaceHolder1_hfYearValue").val();
    var YearMonthReport = YearMonth.split(",");
    var ValuesArr = YearMonthValue.split(",");
    var flotBarsData = [
        [YearMonthReport[11], parseFloat(ValuesArr[11])],
                           [YearMonthReport[10], parseFloat(ValuesArr[10])],
                           [YearMonthReport[9], parseFloat(ValuesArr[9])],
                           [YearMonthReport[8], parseFloat(ValuesArr[8])],
                           [YearMonthReport[7], parseFloat(ValuesArr[7])],
                           [YearMonthReport[6], parseFloat(ValuesArr[6])],
                           [YearMonthReport[5], parseFloat(ValuesArr[5])],
                           [YearMonthReport[4], parseFloat(ValuesArr[4])],
                           [YearMonthReport[3], parseFloat(ValuesArr[3])],
                           [YearMonthReport[2], parseFloat(ValuesArr[2])],
                           [YearMonthReport[1], parseFloat(ValuesArr[1])],
                           [YearMonthReport[0], parseFloat(ValuesArr[0])],
    ];
    /*
	Flot: Bars
	*/
    (function () {



        var plot = $.plot('#flotBars', [flotBarsData], {
            colors: ['#8CC9E8'],
            series: {
                bars: {
                    show: true,
                    barWidth: 0.8,
                    align: 'center'
                }
            },
            xaxis: {
                mode: 'categories',
                tickLength: 0
            },
            grid: {
                hoverable: true,
                clickable: true,
                borderColor: 'rgba(0,0,0,0.1)',
                borderWidth: 1,
                labelMargin: 15,
                backgroundColor: 'transparent'
            },
            tooltip: true,
            tooltipOpts: {
                content: '%y',
                shifts: {
                    x: -10,
                    y: 20
                },
                defaultTheme: false
            }
        });
    })();

    /*
	Flot: Pie
	*/





    /*
	Chartist: Line Chart - With Tooltips
	*/
    (function () {
        new Chartist.Line('#ChartistLineChartWithTooltips', {
            labels: ['1', '2', '3', '4', '5', '6'],
            series: [{
                name: 'Fibonacci sequence',
                data: [1, 2, 3, 5, 8, 13]
            }, {
                name: 'Golden section',
                data: [1, 1.618, 2.618, 4.236, 6.854, 11.09]
            }]
        });

        var $chart = $('#ChartistLineChartWithTooltips');

        var $toolTip = $chart
			.append('<div class="tooltip"></div>')
			.find('.tooltip')
			.hide();

        $chart.on('mouseenter', '.ct-point', function () {
            var $point = $(this),
				value = $point.attr('ct:value'),
				seriesName = $point.parent().attr('ct:series-name');
            $toolTip.html(seriesName + '<br>' + value).show();
        });

        $chart.on('mouseleave', '.ct-point', function () {
            $toolTip.hide();
        });

        $chart.on('mousemove', function (event) {
            $toolTip.css({
                left: (event.offsetX || event.originalEvent.layerX) - $toolTip.width() / 2 - 10,
                top: (event.offsetY || event.originalEvent.layerY) - $toolTip.height() - 40
            });
        });
    })();

    /*
	Chartist: Line Chart - With Area
	*/
    (function () {
        new Chartist.Line('#ChartistLineChartWithArea', {
            labels: [1, 2, 3, 4, 5, 6, 7, 8],
            series: [
				[5, 9, 7, 8, 5, 3, 5, 4]
            ]
        }, {
            low: 0,
            showArea: true
        });
    })();

    /*
	Chartist: Line Chart - Bi-Polar Chart With Area Only
	*/
    (function () {
        new Chartist.Line('#ChartistBiPolarLineChartWithAreaOnly', {
            labels: [1, 2, 3, 4, 5, 6, 7, 8],
            series: [
				[1, 2, 3, 1, -2, 0, 1, 0],
				[-2, -1, -2, -1, -2.5, -1, -2, -1],
				[0, 0, 0, 1, 2, 2.5, 2, 1],
				[2.5, 2, 1, 0.5, 1, 0.5, -1, -2.5]
            ]
        }, {
            high: 3,
            low: -3,
            showArea: true,
            showLine: false,
            showPoint: false,
            fullWidth: true,
            axisX: {
                showLabel: false,
                showGrid: false
            }
        });
    })();

    /*
	Chartist: Line Chart - Using Events to Replace Graphics
	*/
    (function () {
        var chart = new Chartist.Line('#ChartistEventsToReplaceGraphics', {
            labels: [1, 2, 3, 4, 5],
            series: [
				[12, 9, 7, 8, 5]
            ]
        });

        // Listening for draw events that get emitted by the Chartist chart
        chart.on('draw', function (data) {
            // If the draw event was triggered from drawing a point on the line chart
            if (data.type === 'point') {
                // We are creating a new path SVG element that draws a triangle around the point coordinates
                var triangle = new Chartist.Svg('path', {
                    d: ['M',
						data.x,
						data.y - 15,
						'L',
						data.x - 15,
						data.y + 8,
						'L',
						data.x + 15,
						data.y + 8,
						'z'
                    ].join(' '),
                    style: 'fill-opacity: 1'
                }, 'ct-area');

                // With data.element we get the Chartist SVG wrapper and we can replace the original point drawn by Chartist with our newly created triangle
                data.element.replace(triangle);
            }
        });
    })();

    /*
	Chartist: Line Chart - Interpolation / Smoothing
	*/
    (function () {
        var chart = new Chartist.Line('#ChartistLineInterpolationSmoothing', {
            labels: [1, 2, 3, 4, 5],
            series: [
				[1, 5, 10, 0, 1, 2],
				[10, 15, 0, 1, 2, 3]
            ]
        }, {
            // Remove this configuration to see that chart rendered with cardinal spline interpolation
            // Sometimes, on large jumps in data values, it's better to use simple smoothing.
            lineSmooth: Chartist.Interpolation.simple({
                divisor: 2
            }),
            fullWidth: true,
            low: 0
        });
    })();

    /*
	Chartist: Bar Chart - Bi-Polar Chart
	*/
    (function () {
        var data = {
            labels: ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'W8', 'W9', 'W10'],
            series: [
				[1, 2, 4, 8, 6, -2, -1, -4, -6, -2]
            ]
        };

        var options = {
            high: 10,
            low: -10,
            axisX: {
                labelInterpolationFnc: function (value, index) {
                    return index % 2 === 0 ? value : null;
                }
            }
        };

        new Chartist.Bar('#ChartistBiPolarBarChart', data, options);
    })();

    /*
	Chartist: Bar Chart - Overlapping On Mobile
	*/
    (function () {
        var data = {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'Mai', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            series: [
				[5, 4, 3, 7, 5, 10, 3, 4, 8, 10, 6, 8],
				[3, 2, 9, 5, 4, 6, 4, 6, 7, 8, 7, 4]
            ]
        };

        var options = {
            seriesBarDistance: 10
        };

        var responsiveOptions = [
			['screen and (max-width: 640px)', {
			    seriesBarDistance: 5,
			    axisX: {
			        labelInterpolationFnc: function (value) {
			            return value[0];
			        }
			    }
			}]
        ];

        new Chartist.Bar('#ChartistOverlappingBarsOnMobile', data, options, responsiveOptions);
    })();

    /*
	Chartist: Bar Chart - Add Peak Circles Using Draw Events
	*/
    (function () {
        // Create a simple bi-polar bar chart
        var chart = new Chartist.Bar('#ChartistAddPeakCirclesUsingDrawEvents', {
            labels: ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'W8', 'W9', 'W10'],
            series: [
				[1, 2, 4, 8, 6, -2, -1, -4, -6, -2]
            ]
        }, {
            high: 10,
            low: -10,
            axisX: {
                labelInterpolationFnc: function (value, index) {
                    return index % 2 === 0 ? value : null;
                }
            }
        });

        // Listen for draw events on the bar chart
        chart.on('draw', function (data) {
            // If this draw event is of type bar we can use the data to create additional content
            if (data.type === 'bar') {
                // We use the group element of the current series to append a simple circle with the bar peek coordinates and a circle radius that is depending on the value
                data.group.append(new Chartist.Svg('circle', {
                    cx: data.x2,
                    cy: data.y2,
                    r: Math.abs(data.value) * 2 + 5
                }, 'ct-slice'));
            }
        });
    })();

    /*
	Chartist: Bar Chart - Multi-Line Labels
	*/
    (function () {
        new Chartist.Bar('#ChartistMultiLineLabels', {
            labels: ['First quarter of the year', 'Second quarter of the year', 'Third quarter of the year', 'Fourth quarter of the year'],
            series: [
				[60000, 40000, 80000, 70000],
				[40000, 30000, 70000, 65000],
				[8000, 3000, 10000, 6000]
            ]
        }, {
            seriesBarDistance: 10,
            axisX: {
                offset: 60
            },
            axisY: {
                offset: 80,
                labelInterpolationFnc: function (value) {
                    return value + ' CHF'
                },
                scaleMinSpace: 15
            }
        });
    })();

    /*
	Chartist: Bar Chart - Stacked Chart
	*/
    (function () {
        new Chartist.Bar('#ChartistStackedChart', {
            labels: ['Q1', 'Q2', 'Q3', 'Q4'],
            series: [
				[800000, 1200000, 1400000, 1300000],
				[200000, 400000, 500000, 300000],
				[100000, 200000, 400000, 600000]
            ]
        }, {
            stackBars: true,
            axisY: {
                labelInterpolationFnc: function (value) {
                    return (value / 1000) + 'k';
                }
            }
        }).on('draw', function (data) {
            if (data.type === 'bar') {
                data.element.attr({
                    style: 'stroke-width: 30px'
                });
            }
        });
    })();

    /*
	Chartist: Bar Chart - Horizontal Chart
	*/
    (function () {
        new Chartist.Bar('#ChartistHorizontalChart', {
            labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            series: [
				[5, 4, 3, 7, 5, 10, 3],
				[3, 2, 9, 5, 4, 6, 4]
            ]
        }, {
            seriesBarDistance: 10,
            reverseData: true,
            horizontalBars: true,
            axisY: {
                offset: 70
            }
        });
    })();

    /*
	Chartist:
	*/
    var MonthReport = $("#ContentPlaceHolder1_hfMonthReport").val();
    var MonthValues = $("#ContentPlaceHolder1_hfMonthReportValues").val();
    var DayReportArray = MonthReport.split(",");
    var values = MonthValues;
    var ValuesArr = values.split(",");
    var myMontynumber = ValuesArr.length;
    var arrMonth = new Array(mynumber);

    for (var i = 0; i < myMontynumber; i++) {
        arrMonth[i] = parseFloat(ValuesArr[i]);

    }

    (function () {

        if (myMontynumber == 1) {
            new Chartist.Bar('#ChartistExtremeResponsiveConfiguration', {

                labels: [MonthReport],

                series: [

                    [arrMonth[0]],
                ]

            },
                {
                    // Default mobile configuration
                    stackBars: true,
                    axisX: {
                        labelInterpolationFnc: function (value) {
                            return value.split(/\s+/).map(function (word) {
                                return word[0];
                            }).join('');
                        }
                    },
                    axisY: {
                        offset: 20
                    }
                }, [
                // Options override for media > 400px
                ['screen and (min-width: 400px)', {
                    reverseData: true,
                    horizontalBars: true,
                    axisX: {
                        labelInterpolationFnc: Chartist.noop
                    },
                    axisY: {
                        offset: 60
                    }
                }],
                // Options override for media > 800px
                ['screen and (min-width: 800px)', {
                    stackBars: false,
                    seriesBarDistance: 10
                }],
                // Options override for media > 1000px
                ['screen and (min-width: 1000px)', {
                    reverseData: false,
                    horizontalBars: false,
                    seriesBarDistance: 15
                }]
                ]);
        }
        else if (myMontynumber == 2) {
            new Chartist.Bar('#ChartistExtremeResponsiveConfiguration', {

                labels: [MonthReport],

                series: [

                    [arrMonth[0]],
                    [arrMonth[1]],


                ]

            },
          {
              // Default mobile configuration
              stackBars: true,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value.split(/\s+/).map(function (word) {
                          return word[0];
                      }).join('');
                  }
              },
              axisY: {
                  offset: 20
              }
          }, [
          // Options override for media > 400px
          ['screen and (min-width: 400px)', {
              reverseData: true,
              horizontalBars: true,
              axisX: {
                  labelInterpolationFnc: Chartist.noop
              },
              axisY: {
                  offset: 60
              }
          }],
          // Options override for media > 800px
          ['screen and (min-width: 800px)', {
              stackBars: false,
              seriesBarDistance: 10
          }],
          // Options override for media > 1000px
          ['screen and (min-width: 1000px)', {
              reverseData: false,
              horizontalBars: false,
              seriesBarDistance: 15
          }]
          ]);
        }
        else if (myMontynumber == 3) {
            new Chartist.Bar('#ChartistExtremeResponsiveConfiguration', {

                labels: [MonthReport],

                series: [

                    [arrMonth[0]],
                    [arrMonth[1]],
                    [arrMonth[3]]


                ]

            },
          {
              // Default mobile configuration
              stackBars: true,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value.split(/\s+/).map(function (word) {
                          return word[0];
                      }).join('');
                  }
              },
              axisY: {
                  offset: 20
              }
          }, [
          // Options override for media > 400px
          ['screen and (min-width: 400px)', {
              reverseData: true,
              horizontalBars: true,
              axisX: {
                  labelInterpolationFnc: Chartist.noop
              },
              axisY: {
                  offset: 60
              }
          }],
          // Options override for media > 800px
          ['screen and (min-width: 800px)', {
              stackBars: false,
              seriesBarDistance: 10
          }],
          // Options override for media > 1000px
          ['screen and (min-width: 1000px)', {
              reverseData: false,
              horizontalBars: false,
              seriesBarDistance: 15
          }]
          ]);
        }
        else if (myMontynumber == 4) {
            new Chartist.Bar('#ChartistExtremeResponsiveConfiguration', {

                labels: [MonthReport],

                series: [

                    [arrMonth[0]],
                    [arrMonth[1]],
                     [arrMonth[2]],
                     [arrMonth[3]],


                ]

            },
          {
              // Default mobile configuration
              stackBars: true,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value.split(/\s+/).map(function (word) {
                          return word[0];
                      }).join('');
                  }
              },
              axisY: {
                  offset: 20
              }
          }, [
          // Options override for media > 400px
          ['screen and (min-width: 400px)', {
              reverseData: true,
              horizontalBars: true,
              axisX: {
                  labelInterpolationFnc: Chartist.noop
              },
              axisY: {
                  offset: 60
              }
          }],
          // Options override for media > 800px
          ['screen and (min-width: 800px)', {
              stackBars: false,
              seriesBarDistance: 10
          }],
          // Options override for media > 1000px
          ['screen and (min-width: 1000px)', {
              reverseData: false,
              horizontalBars: false,
              seriesBarDistance: 15
          }]
          ]);
        }

        else {
            new Chartist.Bar('#ChartistExtremeResponsiveConfiguration', {

                labels: [MonthReport],

                series: [

                    [arrMonth[0]],
                    [arrMonth[1]],
                    [arrMonth[2]],
                      [arrMonth[3]],
                      [arrMonth[4]]


                ]

            },
          {
              // Default mobile configuration
              stackBars: true,
              axisX: {
                  labelInterpolationFnc: function (value) {
                      return value.split(/\s+/).map(function (word) {
                          return word[0];
                      }).join('');
                  }
              },
              axisY: {
                  offset: 20
              }
          }, [
          // Options override for media > 400px
          ['screen and (min-width: 400px)', {
              reverseData: true,
              horizontalBars: true,
              axisX: {
                  labelInterpolationFnc: Chartist.noop
              },
              axisY: {
                  offset: 60
              }
          }],
          // Options override for media > 800px
          ['screen and (min-width: 800px)', {
              stackBars: false,
              seriesBarDistance: 10
          }],
          // Options override for media > 1000px
          ['screen and (min-width: 1000px)', {
              reverseData: false,
              horizontalBars: false,
              seriesBarDistance: 15
          }]
          ]);
        }

    })();

    /*
	Chartist: Pie Chart - Simple Chart
	*/
    (function () {
        var data = {
            series: [5, 3, 4]
        };

        var sum = function (a, b) {
            return a + b
        };

        new Chartist.Pie('#ChartistSimplePieChart', data, {
            labelInterpolationFnc: function (value) {
                return Math.round(value / data.series.reduce(sum) * 100) + '%';
            }
        });
    })();

    /*
	Chartist: Pie Chart - With Custom Labels
	*/


    var DayReport = $("#ContentPlaceHolder1_hfDayReport").val();
    var DayValues = $("#ContentPlaceHolder1_hfDayReportValues").val();
    var DayReportArray = DayReport.split(",");
    var values = DayValues;
    var ValuesArr = values.split(",");
    var mynumber = ValuesArr.length;
    var arr = new Array(mynumber);

    for (var i = 0; i < mynumber; i++) {
        arr[i] = parseFloat(ValuesArr[i]);

    }

    (function () {

        if (mynumber == 1) {
            var data = {
                labels: [DayReportArray[0]],
                series: [arr[0]]
            };
        }
        if (mynumber == 2) {
            var data = {
                labels: [DayReportArray[0], DayReportArray[1]],
                series: [arr[0], arr[1]]
            };
        }
        if (mynumber == 3) {
            var data = {
                labels: [DayReportArray[0], DayReportArray[1], DayReportArray[2]],
                series: [arr[0], arr[1], arr[2]]
            };
        }
        if (mynumber == 4) {
            var data = {
                labels: [DayReportArray[0], DayReportArray[1], DayReportArray[2], DayReportArray[3]],
                series: [arr[0], arr[1], arr[2], arr[3]]
            };
        }
        if (mynumber == 5) {
            var data = {
                labels: [DayReportArray[0], DayReportArray[1], DayReportArray[2], DayReportArray[3], DayReportArray[4]],
                series: [arr[0], arr[1], arr[2], arr[3], arr[4]]
            };
        }
        var options = {
            labelInterpolationFnc: function (value) {
                return value[0]
            }
        };
        var responsiveOptions = [
			['screen and (min-width: 640px)', {
			    chartPadding: 30,
			    labelOffset: 100,
			    labelDirection: 'explode',
			    labelInterpolationFnc: function (value) {
			        return value;
			    }
			}],
			['screen and (min-width: 1024px)', {
			    labelOffset: 80,
			    chartPadding: 20
			}]
        ];
        new Chartist.Pie('#flotPie', data, options, responsiveOptions);
    })();

    /*
	Chartist: Gauge Chart
	*/
    (function () {
        new Chartist.Pie('#ChartistGaugeChart', {
            series: [20, 10, 30, 40]
        }, {
            donut: true,
            donutWidth: 60,
            startAngle: 270,
            total: 200,
            showLabel: false
        });
    })();

    /*
	Chartist: CSS Animation
	*/
    (function () {
        var data = {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            series: [
				[1, 2, 2.7, 0, 3, 5, 3, 4, 8, 10, 12, 7],
				[0, 1.2, 2, 7, 2.5, 9, 5, 8, 9, 11, 14, 4],
				[10, 9, 8, 6.5, 6.8, 6, 5.4, 5.3, 4.5, 4.4, 3, 2.8]
            ]
        };

        var responsiveOptions = [
			[
				'only screen', {
				    axisX: {
				        labelInterpolationFnc: function (value, index) {
				            // Interpolation function causes only every 2nd label to be displayed
				            if (index % 2 !== 0) {
				                return false;
				            } else {
				                return value;
				            }
				        }
				    }
				}
			]
        ];

        new Chartist.Line('#ChartistCSSAnimation', data, null, responsiveOptions);
    })();

}).apply(this, [jQuery]);