var slider_ids = [];
var $ = jQuery.noConflict();
var description_ids = [];
	$(window).load(function(){ //$(window).load() must be used instead of $(document).ready() because of Webkit compatibility		
	/*Here you can customize the plugin:	*/
	 var slider_frame = $('.jps-panels li');
	 element_push(slider_frame, slider_ids);	
	 element_push_desc(slider_frame, description_ids);	
	/*animating navigation buttons*/
	$('.contentslider-std .jps-go-prev').animate({left:0},800);
	$('.contentslider-std .jps-go-next').animate({right:0},800);
	$('.contentslider-std .jps-go-btn a').hover(
		function(){$(this).stop(true, false).fadeTo(800, 0.75) },
		function(){$(this).stop(true, false).fadeTo(800, 1)}
	);
	$('.jps-go-next').addClass('next');
	$('.jps-go-prev').addClass('prev');
	$('.jps-go-next').append('<strong>&nbsp;</strong>');
	$('.jps-go-prev').append('<strong>&nbsp;</strong>');
	/*animating preview images*/
	$('.jps-go-next').hover(
		function (){$('.jps-go-next strong').stop(true, false).animate({
		opacity: 1,
		}, 800, function() {});},
		function (){$('.jps-go-next  strong').stop(true, false).animate({
		opacity: 0,
		}, 800, function() {});}
	);
	$('.jps-go-prev').hover(
		function (){$('.jps-go-prev strong').stop(true, false).animate({
		opacity: 1,
		}, 800, function() {});},
		function (){
			$('.jps-go-prev  strong').stop(true, false).animate({ opacity: 0}, 800);
		
		}
	);
	/*populate preview images after page loads*/
	populate_preview_images(slider_ids);
	
	  	
	
	var img = new Image();
	img.src ='lib/images/a.jpg';	
	img.onload = function(){
		var panel_height = $('.jps-panel a.holder ').height();
	
			$('.jps-panels, .jps').css('height', panel_height -1)
	
	};

	


	$(window).resize(function() {
		if($('.jps-panel-old').length == 0){
			var src = $('.jps-panel-active img').attr('src');
			var img = new Image();
			img.src = src;	
			img.onload = function(){
				var panel_height = $('.jps-panel-active a.holder img').height();
				$('.jps-panels, .jps').css('height', panel_height -1)
			};
		} else{
				var src = $('.jps-panel-old img').attr('src');
				var img = new Image();
				img.src = src;	
				img.onload = function(){
					var panel_height = $('.jps-panel-old a.holder img').height();
					$('.jps-panels, .jps').css('height', panel_height -1)
				};
		}
	});
	$('.jps-go-next').click(function(){slider_ids.push(slider_ids.shift()); description_ids.push(description_ids.shift());  return false; } );
	$('.jps-go-prev ').click(function(){slider_ids.unshift(slider_ids.pop()); description_ids.unshift(description_ids.pop()); return false });
	
	});
/*************** functions ****************/
/* populating the preview images*/
	function populate_preview_images(sliders){
		var prev = $("#" + sliders[sliders.length-1]);
		$('.jps-go-prev strong').attr('id', sliders[sliders.length-1] + '-thumb');
		$('.jps-go-next strong').attr('id', sliders[1] + '-thumb');
	}
/* function that centers elements $('the element that has to be centered').center();*/
	jQuery.fn.center = function(loaded) {
	    var objs = this;
	    body_width = parseInt($(window).width());
	    objs.each(function() {
	        var obj = $(this)
	        var block_width = parseInt(obj.width());
	        var left_position = parseInt((body_width/2) - (block_width/2)  + $(window).scrollLeft());
	        if (body_width < block_width) { left_position = 0 };
	        if(!loaded) {
	            obj.css({'position': 'absolute'});
	            obj.css({'left': left_position});
	            obj.center(!loaded);
	            $(window).bind('resize', function() { 
	                obj.center(!loaded);
	            });
	            $(window).bind('scroll', function() { 
	                obj.center(!loaded);
	            });
	        } else {
	            obj.stop();
	            obj.css({'position': 'absolute'});
	            obj.animate({'left': left_position}, 200, 'linear');
	        }
	    });
	}
/* elements push */
	function element_push(x,y){
		$(x).each(function(){
			y.push($(this).attr('id'));
		})
	}
	function element_push_desc(x,y){
		$(x).each(function(){
			y.push($(this).find('.description').attr('id'));
		})
	}
	
	