import { trigger, animate, transition, style, group, query ,animateChild} from '@angular/animations';

// export const slideInAnimation = trigger('slideInAnimation', [
//   transition('HomePage <=> AboutPage', [
//     style({ position: 'relative' }),
//     query(':enter, :leave', [
//       style({
//         position: 'absolute',
//         top: 0,
//         left: 0,
//         width: '100%'
//       })
//     ]),
//     query(':enter', [
//       style({ left: '-100%' })
//     ]),
//     query(':leave', animateChild()),
//     group([
//       query(':leave', [
//         animate('300ms ease-out', style({ left: '100%' }))
//       ]),
//       query(':enter', [
//         animate('300ms ease-out', style({ left: '0%' }))
//       ]),
//     ]),
//   ]),
//   transition('* <=> *', [
//     style({ position: 'relative' }),
//     query(':enter, :leave', [
//       style({
//         position: 'absolute',
//         top: 0,
//         left: 0,
//         width: '100%'
//       })
//     ]),
//     query(':enter', [
//       style({ left: '-100%' })
//     ]),
//     query(':leave', animateChild()),
//     group([
//       query(':leave', [
//         animate('200ms ease-out', style({ left: '100%', opacity: 0 }))
//       ]),
//       query(':enter', [
//         animate('300ms ease-out', style({ left: '0%' }))
//       ]),
//       query('@*', animateChild())
//     ]),
//   ])

// ])


// export const slideInAnimation = trigger('slideInAnimation', [
//   // Transition between any two states
//   transition('* => isLeft' , slideTo('left')),
//   transition('* => isRight' , slideTo('right')),
//   transition('isRight => *' , slideTo('left')),
//   transition('isLeft => *' , slideTo('right')),
// ]);

// function slideTo(direction:any){
//   const optional={ optional: true };
//   return[
//     query(':enter, :leave', [
//       style({ position: 'fixed', width: '100%', zIndex: 2 , [direction] :0})
//     ],optional),
//     query(':enter', [
//       style({ [direction]: '0%'})
//     ]),
//     group([
//       query(':leave', [
//         animate('600ms ease-out', style({ [direction]: '100%'}))
//       ], optional),
//       query(':enter', [
//         animate('600ms ease-out', style({  [direction]: '-100%' }))
//       ])
//     ])

//   ]

// }
export const slideInAnimation = trigger('slideInAnimation', [
transition('* <=> *', [
  // Events to apply
  // Defined style and animation function to apply
  // Config object with optional set to true to handle when element not yet added to the DOM
  query(':enter, :leave', style({ position: 'fixed', width: '100%', zIndex: 2 }), { optional: true }),
  // group block executes in parallel
  group([
    query(':enter', [
      style({ transform: 'translateX(100%)' }),
      animate('300ms ease-out', style({ transform: 'translateX(0%)' }))
    ], { optional: true }),
    query(':leave', [
      style({ transform: 'translateX(0%)' }),
      animate('300ms ease-out', style({ transform: 'translateX(-100%)' }))
    ], { optional: true })
  ])
])
])
