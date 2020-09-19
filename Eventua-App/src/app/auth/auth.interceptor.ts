import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { tap } from 'rxjs/internal/operators/tap';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root'})
export class AuthInterceptor implements HttpInterceptor{

    constructor(private router: Router) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (localStorage.getItem('token') !== null) {
            const reqClonada = req.clone(
                { headers: req.headers.set('Authorization', `Bearer ${localStorage.getItem('token')}`)}
            );

            return next.handle(reqClonada).pipe(
                tap(
                    succ => { },
                    err => {
                        if (err.status === 401) {
                            this.router.navigateByUrl('user/login');
                        }
                    }
                )
            );
        } else {
            return next.handle(req.clone());
        }
    }
}
