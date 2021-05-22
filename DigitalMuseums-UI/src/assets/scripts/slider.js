var carousel = (function(){

setTimeout(function(){
var image_holder = document.getElementById('main-slider');
var img_array = image_holder.querySelectorAll('.slider-wrapper img');
var imageWidth = img_array[0].width; 
var imageHeight = img_array[0].height;  

function resizeBg(element_for_reszie) {
  
    var  maxWidth = document.body.clientWidth;
    var  maxHeight = document.body.clientHeight;  
     var aspectRatio = imageWidth /imageHeight;
    if (typeof(element_for_reszie) != 'undefined' && (element_for_reszie != null) ) {
        if ( (maxWidth / maxHeight) < aspectRatio ) {
            element_for_reszie.classList.remove("bgwidth");
            element_for_reszie.classList.add("bgheight");
        } else {
            element_for_reszie.classList.remove("bgheight");
            element_for_reszie.classList.add("bgwidth");
        }  
    }
};

for (var i = 0; i < img_array.length; i++) { 
    resizeBg(img_array[i]);
}

window.addEventListener('resize', function(event){
  for (var i = 0; i < img_array.length; i++) { 
      resizeBg(img_array[i]);
  }
});

var box = document.getElementById('main-slider');
var prev = box.querySelector('#prev');
var next = box.querySelector('#next');
var counter = 0;
var items = box.querySelectorAll('.slider-wrapper img');
var amount = items.length;
$(".preview_count_image_second").html(amount);
$(".preview_count_image_first").html(1);

var current = items[0];
current.classList.add('current');
function navigate(direction) {
  current.classList.remove('current');
  counter = counter + direction;
  if  (direction === -1  && counter < 0) {
    counter = amount - 1; // start at 0, this last item
  }
  if( direction === 1 && !items[counter]) {
      counter = 0;
  }
  $(".preview_count_image_first").html(counter+1);
   current = items[counter];
   current.classList.add('current');
  
}

next.addEventListener('click', function(e){
  navigate(1);
});

 prev.addEventListener('click', function(e){
  navigate(-1);
});
 }, 100);
});