import { InboxOutlined } from '@ant-design/icons';
import { Button, Form, Input, Upload, UploadFile } from 'antd';
import { useTranslation } from 'react-i18next';

import useMaterialFileRules from './use-material-file.rules';
import { ErrorForm } from '../../common/error';
import { COURSE_MATERIAL_RULES } from '../../common/rules';
import { getFileListFromEvent } from '../../utils/helpers';

export interface MaterialFileFormValues {
  title: string;
  files: UploadFile[];
}

export interface MaterialFileSubmitValues {
  title: string;
  file: UploadFile;
}

interface Props {
  initialValues: MaterialFileFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: MaterialFileSubmitValues) => Promise<void>;
}

export function MaterialFileForm({
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<MaterialFileFormValues>();
  const rules = useMaterialFileRules();

  const handleFinish = async ({ title, files }: MaterialFileFormValues) => {
    if (!files.length) {
      return;
    }

    await onSubmit({ title, file: files[0] });
  };

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={handleFinish}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="title"
        label={t('course_material.title')}
        rules={rules.title}
      >
        <Input showCount maxLength={COURSE_MATERIAL_RULES.file.title.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="files"
        label={t('course_material.file')}
        valuePropName="fileList"
        rules={rules.file}
        getValueFromEvent={getFileListFromEvent}
      >
        <Upload.Dragger maxCount={1} listType="text" beforeUpload={() => false}>
          <p className="ant-upload-drag-icon">
            <InboxOutlined />
          </p>
          <p className="ant-upload-text">
            {t('course_material.file_upload_area')}
          </p>
          <p className="ant-upload-hint">Максимальний розмір файлу 50 MB.</p>
        </Upload.Dragger>
      </Form.Item>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
