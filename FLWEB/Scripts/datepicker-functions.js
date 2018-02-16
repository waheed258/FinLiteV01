$(function()
{
    $('.sandbox-container-full').datepicker(
    {
        format: "dd-mm-yyyy",
        todayHighlight: true,
        autoclose: true
    });
});

$(function()
{
    $('.sandbox-container').datepicker(
    {
        format: "dd-mm-yyyy",
        endDate: "today",
        todayHighlight: true,
        autoclose: true
    });
});

$(function()
{
    $('.sandbox-container-today').datepicker(
    {
        format: "yyyy-mm-dd",
    startDate: "today",
    endDate: "+3m",
    autoclose: true,
    todayHighlight: true
    });
});
    
$(function()
{
    $('.sandbox-container-dob').datepicker(
    {
        format: "dd-mm-yyyy",
        endDate: "-5y",
        startView: 2,
        autoclose: true
    });
});

$(function()
{
    $('.sandbox-container-next').datepicker(
    {
        format: "dd-mm-yyyy",
        startDate: "+1d",
        todayHighlight: true,
        autoclose: true
    });
});