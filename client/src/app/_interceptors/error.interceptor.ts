import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  //router=>we use this class because for certain type of errors, we're going to want to redirect the user to a error page
  //toastr=> for certain type of errors, we might want to display a toastr notification
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        if (error) {
          switch(error.status){

            case 400:
              if (error.error.errors) {
                const modalStateErrors = [];
                for (const key in error.error.errors) { //because it can be more than one reason that generates 400 error
                  if(error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat(); //throw the modal state errors back to the component
              } else {
                this.toastr.error(error.statusText, error.status);
              }  
              break;
            
            case 401:
              this.toastr.error(error.statusText === "OK" ? "Unauthorized" : error.statusText, error.status);
            break;

            case 404:
              this.router.navigateByUrl("/not-found");
            break;
            
            case 500:
              const navigationExtras: NavigationExtras = {state: {error: error.error}}; //to pass the details of the error
              this.router.navigateByUrl('/server-error', navigationExtras);
            break;

            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        return throwError(error);
      })
    )
  }
}
