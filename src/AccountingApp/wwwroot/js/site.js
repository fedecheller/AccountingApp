$(document).ready(function () {
    $('#startDateTimePicker').datepicker({
        daysOfWeekDisabled: [0, 2, 3, 4, 5, 6]
        ,format: 'dd/mm/yyyy'
    }).on('changeDate', function (e) {
        var date = moment($('#startDateTimePicker').datepicker('getDate')).format('YYYY-MM-DD');
        $('#startdatefilter').val(date);
        $(this).closest("form").submit();
    });

    $('.checktime').click(function () {
        var checkedTime = $(this).next().val();
        var times = ($('#times').val()) ? $('#times').val().split(',') : [];
        var checkedHour = $(this).next().next().val();
        var currentAmount = parseInt($('#bill_Amount').val());
        var newAmount = (checkedHour * parseInt($('#hourprice').val()));
        if (this.checked) {
            times.push(checkedTime);
        } else {
            var index = times.indexOf(checkedTime);
            if (index > -1) {
                times.splice(index, 1);
            }
            newAmount = -newAmount;
        }
        $('#times').val(times);
        $('#bill_Amount').val(currentAmount + newAmount);
        $("#bill_Amount").trigger("change");
    });

    $('.print-btn').click(function () {
        window.print();
        return false;
    });

    $('.bill-create').click(function () {
        if ($('#times').val() == '') {
            alert('Need to select at least one time entry');
            return false;
        }
        return true;
    });

    $('#bill_Number').change(function () {
        $('#number').val($(this).val());
    });
    $('#bill_Amount').change(function () {
        $('#amount').val($(this).val());
    });
    $('#bill_Date').change(function () {
        $('#date').val($(this).val());
    });
    $("#bill_Number").trigger("change");
    $("#bill_Amount").trigger("change");
    $("#bill_Date").trigger("change");
});