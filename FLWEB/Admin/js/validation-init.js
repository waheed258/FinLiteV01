var Script = function () {
    $().ready(function() {
        // validate login form on keyup and submit
        $("#LoginForm").validate({
            rules: {
                txtLoginId: "required",
                txtPassword: "required"
            },
            messages: {
                txtLoginId: "Please enter your UserId",
                txtPassword: "Please enter your Password"
            }
        });
        
//        $("#ForgotPassword").validate({
//            rules: {
//                txtEmail: {
//                    required: true,
//                    email: true
//                },
//                txtLoginId2: "required"
//            },
//            messages: {
//                txtEmail:{
//                    required: "Please enter your register email id.",
//                    email: "Please enter a valid email address."
//                },
//                txtLoginId2: "Please enter your UserId"
//            }
//        });
        
    });
    
}();