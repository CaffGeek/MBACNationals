app.directive('droppable', function () {
    return {
        scope: {
            drop: '&'
        },
        link: function (scope, element, attr) {
            // again we need the native object
            var el = element[0];

            el.addEventListener(
              'dragover',
              function (e) {
                  e.dataTransfer.dropEffect = 'move';
                  // allows us to drop
                  if (e.preventDefault) e.preventDefault();
                  this.classList.add('over');
                  return false;
              },
              false
            );

            el.addEventListener(
              'dragenter',
              function (e) {
                  this.classList.add('over');
                  return false;
              },
              false
            );

            el.addEventListener(
              'dragleave',
              function (e) {
                  this.classList.remove('over');
                  return false;
              },
              false
            );

            el.addEventListener(
              'drop',
              function (e) {
                  // Stops some browsers from redirecting.
                  if (e.preventDefault) { e.preventDefault(); }
                  if (e.stopPropagation) { e.stopPropagation(); }

                  this.classList.remove('over');

                  var acceptedType = el.getAttribute('data-droptype')
                      || el.getAttribute('droptype')
                      || 'any';

                  var value = $(this).attr('data-droppable');
                  var binId = this.id;
                  var parts = e.dataTransfer.getData('Text').split('||');

                  var type = parts[0];
                  var id = parts[1];

                  if (type != acceptedType)
                      return false;

                  var item = document.getElementById(id);
                  
                  scope.$apply(function (scope) {
                      var fn = scope.drop();
                      if ('undefined' !== typeof fn) {
                          fn(item.id, value || binId);
                      }
                  });

                  return false;
              },
              false
            );
        }
    }
});