import { RestService } from '@ctincsp/ng.core';
import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { BehaviorSubject, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  apiName = 'Default';

  private avatarUrlSubject = new BehaviorSubject<any>(''); // Khởi tạo BehaviorSubject với giá trị ban đầu là rỗng
  avatarUrl$: Observable<string> = this.avatarUrlSubject.asObservable(); // Khai báo một Observable để các component có thể subcribe vào

  getAvatar = (userId: string | null = null) =>
    this.restService.request<any, any>(
      {
        method: 'GET',
        url: `/api/identity/users/avatar/${userId}`,
      },
      { apiName: this.apiName }
    );

  uploadAvatar(file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.restService
      .request<any, string>(
        {
          method: 'POST',
          responseType: 'text',
          url: '/api/identity/users/upload-avatar',
          body: formData,
        },
        { apiName: this.apiName }
      )
      .pipe(
        tap(result => {
          if (result)
            this.getAvatar(result).subscribe(data => {
              if (data) {
                let objectURL = 'data:image/png;base64,' + data;
                let avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
                this.avatarUrlSubject.next(avatarUrl); // Emit giá trị mới vào BehaviorSubject
              }
            });
        })
      );
  }

  uploadFilAttachment(fileAttachmentId: string, file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);

    return this.restService.request<any, string>(
      {
        method: 'POST',
        responseType: 'text',
        url: `/api/app/file-attachment/upload/${fileAttachmentId}`,
        body: formData,
      },
      { apiName: this.apiName }
    );
  }

  downloadFileAttachment = (fileAttachmentId: string) =>
    this.restService.request<any, any>(
      {
        method: 'POST',
        url: `/api/app/file-attachment/download/${fileAttachmentId}`,
      },
      { apiName: this.apiName }
    );
  constructor(private restService: RestService, private sanitizer: DomSanitizer) {}
}
