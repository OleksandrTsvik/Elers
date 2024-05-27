import { InboxOutlined } from '@ant-design/icons';
import { GetProps, Upload } from 'antd';
import { useTranslation } from 'react-i18next';

type UploadDraggerProps = GetProps<typeof Upload.Dragger>;

interface Props extends UploadDraggerProps {
  fileSizeLimitMb: number;
}

export function FileDropzone({ fileSizeLimitMb, ...props }: Props) {
  const { t } = useTranslation();

  return (
    <Upload.Dragger
      maxCount={1}
      listType="text"
      beforeUpload={() => false}
      {...props}
    >
      <p className="ant-upload-drag-icon">
        <InboxOutlined />
      </p>
      <p className="ant-upload-text">{t('file_upload.upload_text')}</p>
      <p className="ant-upload-hint">
        {t('file_upload.upload_hint', { limit: fileSizeLimitMb })}
      </p>
    </Upload.Dragger>
  );
}
