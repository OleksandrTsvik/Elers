import { useTranslation } from 'react-i18next';

import useDisplayError from './use-display-error';
import { fetchBlob, handleDownloadBlob } from '../utils/helpers';

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
      handleDownloadBlob(response.data, fileName);
    }
  };

  return { downloadFile };
}
