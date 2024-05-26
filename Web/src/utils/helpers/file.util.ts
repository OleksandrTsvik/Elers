import { UploadFile } from 'antd';
import { EventArgs } from 'rc-field-form/es/interface';

import { isObject } from './type-guards.util';

export function downloadFileByLink(link: string, fileName: string) {
  const a = document.createElement('a');
  a.href = link;
  a.download = fileName;

  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
}

export function downloadFileByBlob(file: Blob, fileName: string) {
  const url = URL.createObjectURL(file);

  const a = document.createElement('a');
  a.href = url;
  a.download = fileName;

  document.body.appendChild(a);
  a.click();

  document.body.removeChild(a);
  URL.revokeObjectURL(url);
}

export function openFilePdfByLink(link: string) {
  const a = document.createElement('a');
  a.href = link;
  a.target = '_blank';

  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
}

export function getFileListFromEvent(event: EventArgs): UploadFile[] {
  if (!isObject(event) || !('fileList' in event)) {
    return [];
  }

  return event.fileList as UploadFile[];
}
