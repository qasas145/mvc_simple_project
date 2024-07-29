function likePost(postId) {
    console.log("The post id is ", postId);
    $.ajax({
        url: '/Posts/Like',
        type: 'POST',
        data: { id: postId },
        success: function (response) {
            $('#like-count-' + postId).text(response.likes);
        }
    });
}

function sharePost(postId) {
    $.ajax({
        url: '/Posts/Share',
        type: 'POST',
        data: { id: postId },
        success: function (response) {
            $('#share-count-' + postId).text(response.shares);
        }
    });
}

function commentPost(postId) {
    var comment = prompt("Enter your comment:");
    if (comment) {
        $.ajax({
            url: '/Posts/Comment',
            type: 'POST',
            data: { id: postId, comment: comment },
            success: function (response) {
                $('#comments-section-' + postId).append('<p>' + comment + '</p>');
            }
        });
    }
}