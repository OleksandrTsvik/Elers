import { Button, Result } from 'antd';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import useDownloadFile from '../../hooks/use-download-file';

import styles from './pdf.module.scss';

interface Props {
  pdfUrl: string | undefined;
  fileName: string;
  downloadLink: string;
}

export default function PdfContent({ pdfUrl, fileName, downloadLink }: Props) {
  const { t } = useTranslation();
  const { downloadFile } = useDownloadFile();

  const handleDownloadFile = () => downloadFile(downloadLink, fileName);

  return (
    <iframe className={styles.pdfViewer} src={pdfUrl} title={fileName}>
      <Result
        status="warning"
        title={t('pdf_page.iframe_not_supported')}
        extra={
          <>
            <Button type="primary" onClick={handleDownloadFile}>
              {t('pdf_page.download_file')}
            </Button>
            <Link to="/">
              <Button type="default">{t('pdf_page.home_link')}</Button>
            </Link>
          </>
        }
      />
    </iframe>
  );
}
