import { Spin } from 'antd';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useLocation, useNavigate, useSearchParams } from 'react-router-dom';

import PdfContent from './pdf.content';
import PdfHead from './pdf.head';
import useDisplayError from '../../hooks/use-display-error';
import { SEARCH_PARAM_PDF_FILE_NAME } from '../../utils/constants/app.constants';
import { fetchBlob } from '../../utils/helpers';

export default function PdfPage() {
  const { t, i18n } = useTranslation();

  const location = useLocation();
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const { displayError } = useDisplayError();

  const [pdfUrl, setPdfUrl] = useState<string>();
  const [isLoading, setIsLoading] = useState(true);

  const fileName =
    searchParams.get(SEARCH_PARAM_PDF_FILE_NAME) ?? t('pdf_page.title');

  const pathToPdfDownloadApi = location.pathname.slice(
    location.pathname.indexOf('/', 1),
  );

  useEffect(() => {
    fetchBlob(pathToPdfDownloadApi, {
      headers: { 'Accept-Language': i18n.language },
    })
      .then((response) => {
        if (response.status === 'ok') {
          setPdfUrl(URL.createObjectURL(response.data));
        } else {
          navigate('/');
          displayError(response.error);
        }
      })
      .catch((error) => {
        navigate('/');
        displayError(error);
      })
      .finally(() => setIsLoading(false));

    return () => {
      if (pdfUrl) {
        URL.revokeObjectURL(pdfUrl);
      }
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (isLoading) {
    return <Spin fullscreen tip={t('pdf_page.loading')} />;
  }

  return (
    <>
      <PdfHead fileName={fileName} />
      <PdfContent
        pdfUrl={pdfUrl}
        fileName={fileName}
        downloadLink={pathToPdfDownloadApi}
      />
    </>
  );
}
