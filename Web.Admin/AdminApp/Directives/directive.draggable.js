app.directive('draggable', function () {
    return function (scope, element) {
        var el = element[0];

        el.draggable = true;
        var type = el.getAttribute('data-dragtype')
            || el.getAttribute('dragtype')
            || 'any';
        
        el.addEventListener(
          'dragstart',
          function (e) {
              e.dataTransfer.effectAllowed = 'move';
              e.dataTransfer.setData('Text', type + '||' + this.id);
              this.classList.add('drag');
              return false;
          },
          false
        );

        el.addEventListener(
          'dragend',
          function (e) {
              this.classList.remove('drag');
              return false;
          },
          false
        );
    }
});