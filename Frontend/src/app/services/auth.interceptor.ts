import { inject } from '@angular/core';
import { HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';
import { AuthService } from './auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  const clonedRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${authService.obterToken()}`,
    },
  });

  return next(clonedRequest).pipe(
    catchError((error) => {
      // snackBar.open('Ops, houve um erro', 'Fechar', {
      //   duration: 5000,
      // });
      return throwError(() => error);
    })
  );
};
