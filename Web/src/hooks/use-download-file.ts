import { useTranslation } from 'react-i18next';

import useDisplayError from './use-display-error';
import { SEARCH_PARAM_PDF_FILE_NAME } from '../utils/constants/app.constants';
import {
  fetchBlob,
  downloadFileByBlob,
  openFilePdfByLink,
} from '../utils/helpers';

export default function useDownloadFile() {
  const { i18n } = useTranslation();
  const { displayError } = useDisplayError();

  const downloadFile = async (link: string, fileName: string) => {
    const response = await fetchBlob(link, {
      headers: { 'Accept-Language': i18n.language },
    });

    if (response.status === 'failed') {
      displayError(response.error);
    } else {
      downloadFileByBlob(response.data, fileName);
    }
  };

  const downloadFileOrOpenPdf = async (link: string, fileName: string) => {
    const fileExtension = fileName.split('.').pop()?.toLowerCase();

    if (fileExtension === 'pdf') {
      openFilePdfByLink(
        '/pdf' + link + `?${SEARCH_PARAM_PDF_FILE_NAME}=${fileName}`,
      );
      return;
    }

    await downloadFile(link, fileName);
  };

  return { downloadFile, downloadFileOrOpenPdf };
}
