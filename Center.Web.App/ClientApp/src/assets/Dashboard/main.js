$(document).ready(function () {
    $('#side-menu').metisMenu();


    /*
     * Initial calls will be done here
     */
    function init() {
        setPageProperties();
        onWindowResize();
		$('.sidebar-toggler').on('click',function(){
			$(this).toggleClass('is-active');
			 
			$($(this).data().showtarget).toggleClass('show');
		});
    }

	/*
	*  set the margin top for the page container
	*  Set the padding-top for the side bar
	*/
    function setPageProperties() {
        $('#page-container').css("margin-top", $('#page-header').height());
        $('#side-menu').css("padding-top", $('#page-header').height() + 2);
        setContentHeight();
    }

	/*
	* This would be called on window resize 
	* So that the design would not be affect when we don't have content.
	* We will not receive any unnecessary scroll bar.
	*/
    function onWindowResize() {
        $(window).on('resize', function () {
            setContentHeight();
        });
    }
	
	/*
	*	Set the minimum height for the page container and page content wrapper.
	*/
    function setContentHeight() {
        var windowHeight = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height);
        /*
         * page-container
         * side-menu
         * page-content-wrapper
         */
        var contentHeight = (windowHeight - ($('#page-header').height() + 20));
        $('#page-container').css('min-height', (windowHeight - ($('#page-header').height() + 2)));
        $('#page-content-wrapper').css('min-height', (windowHeight - ($('#page-header').height() + 2)));
    }

    init();
});