$(function () {
    var Events;
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: "/Calendar/ShowEvents",
            data: {},
            beforeSend: function () {
                
            },
            success: function (response) {
           
                //alert("Success");
                
                Events = response;
                console.log(Events);

                function MainFunction() {
                   
                    SetWeekdays();
                    var Days = SetDays();
                    var DayOfWeek_INDEX = 0;
                    var u = false;
                    calendar_content.empty();
                    while (!u) {
                        if (DayOfWeek[DayOfWeek_INDEX] == Days[0].weekday) {
                            u = true;
                        }
                        else {
                            calendar_content.append('<div class="blank"></div>');
                            DayOfWeek_INDEX++;
                        }
                    }
                    var MonthLength = Days.length;
                    for (var i = 0; i < 42 - DayOfWeek_INDEX; i++) {
                        if (i >= Days.length) {
                            calendar_content.append('<div class="blank"></div>')
                        }
                        else {
                            var Day = Days[i].day;
                            var m = CheckNewdate(new Date(Year, Month - 1, Day)) ?
                                '<div id="DAY' + (i + 1) + '-MONTH' + Month + '-YEAR' + Year + '" class="today">' :
                                '<div id="DAY' + (i + 1) + '-MONTH' + Month + '-YEAR' + Year + '">';
                            calendar_content.append(m + "" + Day + "</div>");
                        }
                    }
                    var y = MonthColor[Month - 1];
                    calendar_header.css("background-color", "#27ae60").find("h1").text(MONTHS[Month - 1] + " " + Year);
                    calendar_weekdays.find("div").css("color", "#27ae60");
                    calendar_content.find(".today").css("background-color", "#0000ff");
                    for (var i = 0; i < Events.length; i++) {
                        
                        calendar_content.find('#DAY' + moment(Events[i].EventInfo.StartTime).format("D") +
                            '-MONTH' + moment(Events[i].EventInfo.StartTime).format("M") +
                            '-YEAR' + moment(Events[i].EventInfo.StartTime).format("YYYY")).text(Events[i].EventInfo.Description);
                    }

                    SetCSS();
                }

                function SetDays() {
                    var e = [];
                    for (var i = 1; i < GetNewdateDate(Year, Month) + 1; i++) {
                        e.push(
                            {
                                day: i,
                                weekday: DayOfWeek[GetNewdateDay(Year, Month, i)]
                            })
                    }
                    return e;
                }

                function SetWeekdays() {
                    calendar_weekdays.empty();
                    for (var i = 0; i < 7; i++) {
                        calendar_weekdays.append("<div>" + DayOfWeek[i].substring(0, 3) + "</div>")
                    }
                }

                function SetCSS() {
                    var CalendarElementsIDs;
                    var CalendarStyle = $("#calendar").css("width", e + "px");
                    CalendarStyle.find(CalendarElementsIDs = "#calendar_weekdays, #calendar_content").css("width", e + "px").find("div").css(
                        {
                            width: e / 7 + "px",
                            height: e / 7 + "px",
                            "line-height": e / 7 + "px"
                        });
                    CalendarStyle.find("#calendar_header").css(
                        {
                            height: e * (1 / 7) + "px"
                        }).find('a[class^="icon-chevron"]').css("line-height", e * (1 / 7) + "px")
                }

                function GetNewdateDate(year, month) {
                    return (new Date(year, month, 0)).getDate();
                }

                function GetNewdateDay(year, month, day) {
                    return (new Date(year, month - 1, day)).getDay();
                }

                function CheckNewdate(date) {
                    return GetFullDate(new Date) == GetFullDate(date)
                }

                function GetFullDate(date) {
                    return date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate()
                }

                function SetDate() {
                    var date = new Date;
                    Year = date.getFullYear();
                    Month = date.getMonth() + 1;
                }
                var e = 480;
                var Year = 2013;
                var Month = 9;
                var r = [];
                var MONTHS = ["JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER"];
                var DayOfWeek = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
                var MonthColor = ["#16a085", "#1abc9c", "#c0392b", "#27ae60", "#FF6860", "#f39c12", "#f1c40f", "#e67e22", "#2ecc71", "#e74c3c", "#d35400", "#2c3e50"];
                var Calendar = $("#calendar");
                var calendar_header = Calendar.find("#calendar_header");
                var calendar_weekdays = Calendar.find("#calendar_weekdays");
                var calendar_content = Calendar.find("#calendar_content");
                SetDate();
                MainFunction();
                calendar_header.find('a[class^="icon-chevron"]').on("click", function () {
                    var direction = $(this);
                    var SurfDirection = function (direction) {
                        Month = direction == "next" ? Month + 1 : Month - 1;
                        if (Month < 1) {
                            Month = 12;
                            Year--;
                        }
                        else if (Month > 12) {
                            Month = 1;
                            Year++;
                        }
                        MainFunction();
                    };
                    if (direction.attr("class").indexOf("left") != -1) {
                        SurfDirection("previous");
                    }
                    else {
                        SurfDirection("next");
                    }
                })
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
})
