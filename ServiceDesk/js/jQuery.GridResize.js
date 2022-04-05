(function($){
    $.fn.GridResize = function(settings){
        var config = {
            'heightBase': 90 //diferença de altura entre o tamanho da janela e grid (menu no topo, footer, etc)
        };
        if (settings){$.extend(config, settings);}     
		
        var heightGrid = $(window).height() - config.heightBase;
        if ($("#divMensagem").is(':visible'))
        {
            heightGrid -= 24;
        }

		this.height(heightGrid);	

        return this;
    };
})(jQuery);


