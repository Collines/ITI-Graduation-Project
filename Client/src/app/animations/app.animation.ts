
import {
  trigger,
  animate,
  transition,
  style,
  query,
  group
} from '@angular/animations';


export const fadeAnimation =

  trigger('fadeAnimation', [

    transition('* => *', [

      query(':enter',
        [

          style({opacity: 0, position: 'absolute', top: 0, left: 0,right:0 ,width: '100%', background:'transparent', height: '100%', 'z-index' : 50})
        ],
        {optional: true}
      ),

      query(':leave',
        [
          style({opacity: 1}),
          animate('0.3s', style({opacity: 0}))
        ],
        {optional: true}
      ),

      query(':enter',
        [
          style({opacity: 0}),
          animate('0.3s', style({opacity: 1}))
        ],
        {optional: true}
      )

    ])

  ]);


