/* Author:
    Pawe³ So³tysiak
*/

$(function () {
    function connectVoteAction(buttonHandler, voteUrl) {
        $(buttonHandler).unbind("click");
        $(buttonHandler).click(function () {
                
            $.ajax({
                url: voteUrl,
                type: "POST",
                data: "messageId=" + buttonHandler.data('messageid'),
                success:function(data) {
                    refreshButtonStatus(buttonHandler);
                }
            });
        });
    }


    function refreshButtonStatus(btn) {
        $.ajax({
            url: "Message/CheckMessageStatus",
            type: "POST",
            data: "messageId=" + btn.data('messageid'),
            success: function(data) {
                console.log(data.CurrentUserLike);
                btn.removeClass('disabled');

                if (!data.CurrentUserLike) {
                    $(btn).html("&uarr; " + data.VoteCount);
                    connectVoteAction(btn, "Message/Like");
                } else {
                    $(btn).html("&darr; " + data.VoteCount);
                    connectVoteAction(btn, "Message/Dislike");
                }
            }
        });
    }

    $(".votebtn").each(function() {
        var btn = $(this);
        refreshButtonStatus(btn);
    });    

   
});




