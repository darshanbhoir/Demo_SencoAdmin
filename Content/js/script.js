(function ($) {
    'use strict';
    var $window = $(window);
    // :: Accordian Active Code
    (function () {
        var dd = $('dd');
        dd.filter(':nth-child(n+3)').hide();
        $('dl').on('click', 'dt', function () {
            $("dt.v2.wave").removeClass('active')
            $(this).next().slideDown(500).siblings('dd').slideUp(500);
            $(this).addClass('active')
        })
    })();

})(jQuery);

$.urlParam = function(name){
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results==null) {
       return null;
    }
    return decodeURI(results[1]) || 0;
}

if($.urlParam('utm_source') !== null && $.urlParam('utm_campaign') !== null ){
    $('input[name="leadpage"]').val($('input[name="leadpage"]').val()+' ('+ $.urlParam('utm_source')+', '+$.urlParam('utm_campaign') +') ');
}

$(document).ready(function () {
    $('.fgroup input[type=text],.fgroup select,.fgroup textarea').on('focus', function () {
        $(this).closest('.fgroup').addClass('in');
    }).on('blur', function () { 
        if ($(this).val() == '') {
            $(this).closest('.fgroup').removeClass('in');
        }
        else {

        }
    });
	
	// var swiper = new Swiper('.testimonial-slider', {
	//   slidesPerView: 1,
	//   slidesPerGroup: 1,
	//   loop: false,
	//   navigation: {
	// 	nextEl: '.swiper-button-next',
	// 	prevEl: '.swiper-button-prev',
	//   },
	//   autoplay: {
	// 	delay: 5000,
	// 	disableOnInteraction: false,
	//   },
	// });
	
	// ===== Scroll to Top ==== 
	$(window).scroll(function() {
		if ($(this).scrollTop() >= 550) {        // If page is scrolled more than 50px
			$('#return-to-top').fadeIn(200);    // Fade in the arrow
		} else {
			$('#return-to-top').fadeOut(200);   // Else fade out the arrow
		}
	});
	$('#return-to-top').click(function() {      // When arrow is clicked
		$('body,html').animate({
			scrollTop : 0                       // Scroll to top of body
		}, 500);
	});



});
(function($){
    var applyWPIcon = function(){
    var circlediv1 = $("<div class=\"cbh-ph-circle\"></div>")
    var circlediv2 = $("<div class=\"cbh-ph-circle-fill\"></div>")
    var circlediv3 = $("<div class=\"cbh-ph-img-circle1\"></div>")
    
    var wpDiv = $("<div id=\"wpdiv\" class=\"cbh-phone\"><div/>")
    var wpHref = $("<a  class=\"whatsappbtn\" href=\"https://api.whatsapp.com/send?phone=+917715906259&text=Hi, I'm interested in joining EMF Gym\" style=\"text-decoration:none;\" target=\"_blank\"></a>")

    $("body").append(wpDiv); 
    $(wpDiv).html(wpHref);  
    $(".whatsappbtn").append(circlediv1);
    $(".whatsappbtn").append(circlediv2);
    $(".whatsappbtn").append(circlediv3);
    };

    applyWPIcon();
    $(window).resize(function(){
    $("#wpdiv").remove();
    applyWPIcon();
    });
})(jQuery);

	function form_validate_jquery(container)
{
	var return_state = true;	

	$(container).find("input, select, textarea, checkbox,  file " ).each(function(){
			
		var title = $(this).attr("title");		
		$(this).parent().addClass("in");
		switch($(this).attr("validation"))
		{				
			case "text":
				if($(this).val() == $(this).attr("placeholder") || $(this).val() == "")
				{
				
					$(this).css("border-color","red");
					

					$(this).val('');
					
					$(this).attr("placeholder", title+" cannot be blank!")
					
					return_state = false;													
				}
				else
				{
					$(this).css("border-color","green");
					
					$(this).attr("placeholder", title)
				}
			break;


            case "string":
          
                if ($(this).val() == "") {
                    $(this).css("border-color","red");
                    $(this).val("");
                    $(this).attr("placeholder", title+" cannot be blank!")
                    return_state = false;
                } else if (!/^[a-zA-Z ]*$/g.test($(this).val())) {
                    $(this).css("border-color","red");
                    $(this).val("");
                    $(this).attr("placeholder", "Only Alphabets should allowed here.");
                    return_state = false;
                } else {
                     $(this).css("border-bottom","1px solid green");
                     $(this).attr("placeholder", title+'*');
                }
            break;

			case "checkbox":
			
				if($(container).find('input:checkbox:checked').length == 0)
				{
					$(this).next().css({"background": "rgba(223, 15, 15, 0.6)"});
					$('.form_error').show();
					return_state = false;													
				}
				else
				{	
					$('.form_error').hide();
					$(this).next().css({"background": "rgba(0, 0, 0, 0.12)"});
				}
			break;




			case "textarea":
				if($(this).val() == $(this).attr("placeholder") || $(this).val() == "")
				{
					$(this).css("border-color","red");
					$(this).val('');
					
					$(this).attr("placeholder", "This cannot be blank!")
					
					return_state = false;													
				}
				else
				{
					$(this).css("border-color","green");
					$(this).attr("placeholder", title)
				}
			break;
		
			case "password":

				if($(this).val() == "")
				{
					$(this).css("border-color","red");
					$(this).val('');
					
					$(this).attr("placeholder", title+" cannot be blank!")
					
					return_state = false;													
				}
				else if($(this).val().length < 6)
				{
					$(this).css("border-color","red");
					$(this).val('');					
					$(this).attr("placeholder", title + " should be min 6 char!")
					
					return_state = false;		
				}
				else if($(this).attr('match')!="" && $(this).attr('match')!=null && $(this).attr('match')!='undefined'){
					if($(this).closest(container).find('#'+$(this).attr('match')).val()!=$(this).val())
					{
						$(this).css("border-color","red");
						$(this).val('');					
						$(this).attr("placeholder", title+" does not match!")
						
						return_state = false;		
					}else{
						$(this).css("border-color","green");
						$(this).attr("placeholder", title)
					}
				}
				else
				{
					$(this).css("border-color","green");
					$(this).attr("placeholder", title)
				}
			break;
			case "email":
				if($(this).val() == $(this).attr("placeholder") || $(this).val() == "")
				{
					$(this).css("border-color","red");
					$(this).val('');
					$(this).attr("placeholder", title+" cannot be blank!")							
					return_state = false;
				}
				else if($(this).val().indexOf('@') == -1 || $(this).val().indexOf('.') == -1)
				{
					$(this).css("border-color","red");
					$(this).val('');
					$(this).attr("placeholder", "Enter Valid Email address !")							
					return_state = false;
				}
				
				else
				{
					$(this).css("border-color","green");
					$(this).attr("placeholder", title)
				}
			break;
			
			case "mobile":
				if($(this).val() == $(this).attr("placeholder") || $(this).val() == "")
				{
					$(this).css("border-color","red");
					$(this).val('');
					$(this).attr("placeholder", title+" cannot be blank !")							
					return_state = false;
				}
				else if(isNaN($(this).val()))
				{
					$(this).css("border-color","red");
					$(this).val('');
					$(this).attr("placeholder", title+" should be numeric !")							
					return_state = false;
				}
				else if($(this).val().length != 10 )
				{
					$(this).css("border-color","red");
					$(this).val('');
					$(this).attr("placeholder", title+" must be 10 digits !")							
					return_state = false;
				}
				else if($(this).val().charAt(0)=='0' )
				{
					$(this).css("border-color","red");
					$(this).val('');
					$(this).attr("placeholder", title+"  cannot start with zero !")							
					return_state = false;
				}
			
				
				else
				{
					$(this).css("border-color","green");
					$(this).attr("placeholder", title)
				}				
			break;
			case "number":
				if($(this).val() == $(this).attr("placeholder") || $(this).val() == "")
				{
					$(this).prev().addClass('error');
					$(this).val('');
					$(this).attr("placeholder", "This cannot be blank!")							
					return_state = false;
				}
				else if(isNaN($(this).val()))
				{
					$(this).prev().addClass('error');
					$(this).val('');
					$(this).attr("placeholder", "This is not valid number!")							
					return_state = false;
				}
				else
				{
					$(this).prev().addClass('active');
					$(this).attr("placeholder", "")
				}				
			break;
			case "file":
             
                    if($(this).val() == "" )
                    {
                        
                        $(this).closest('.file-select').css("border-bottom","1px solid red");
                        $(this).val('');
                        
                        return_state = false;                                                   
                    }else if($(this).val()!=""){
                      
                        var ext_arr = ['pdf','doc','docx']; 
            
                        
                        var ext = $(this).val().split('.').pop().toLowerCase();
                        if($.inArray(ext, ext_arr) == -1) {
                            $(this).val('');
                            $(this).closest('.file-select').css("border-bottom","1px solid red");
                        
                            $(this).prev().text("Invalid file extension. choose .pdf, .docx  file format.");
                           
                            return_state = false;       
                        }else{
                            $(this).closest('.file-select').css("border-bottom","1px solid green");
                            
                            $(this).prev().text($(this).val().split('\\').pop());
                        }
                    }
                    else
                    {
                        $(this).closest('.file-select').css("border","1px solid green");
                        $(this).closest('.file-select').next().next().text($(this).val().split('\\').pop());
                    }
                break;
		}
	})

   return return_state;

}