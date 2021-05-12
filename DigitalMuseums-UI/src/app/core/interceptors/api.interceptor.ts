import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.prod';
import { AuthUser } from '../auth/models/auth-user.model';
import { CurrentUserService } from '../shared/services/current-user.service';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private currrentUserService: CurrentUserService) {}

  public intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const re = /assets/gi;
    
    // Exclude interceptor for assets:
    if (req.url.search(re) !== -1) {
      return next.handle(req);
    }
    
    if (ApiInterceptor.isAbsoluteUrl(req.url)) {
      return next.handle(req);
    }

    const { baseUrl } = environment;
    const authToken: string = this.currrentUserService.getUserToken();
    const headers = !!authToken
      ? req.headers.append('Authorization', `Bearer ${authToken}`)
      : req.headers;

    const clonedReq: HttpRequest<any> = req.clone({
      url: `${baseUrl}${req.url}`,
      headers: headers,
    });

    return next.handle(clonedReq);
  }

  private static isAbsoluteUrl(urlString: string): boolean {
    const absoluteHttpUriPattern: RegExp = /^https?:\/\//i;

    return absoluteHttpUriPattern.test(urlString);
  }
}
