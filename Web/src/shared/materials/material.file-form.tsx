import { Button, Form, Input, UploadFile } from 'antd';
import { useTranslation } from 'react-i18next';

import useMaterialFileRules from './use-material-file.rules';
import { ErrorForm } from '../../common/error';
import { FileDropzone } from '../../common/file-upload';
import { COURSE_MATERIAL_RULES } from '../../common/rules';
import { FormMode } from '../../common/types';
import { FILE_SIZE_LIMIT_MB } from '../../utils/constants/app.constants';
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
  mode: FormMode;
  initialValues: MaterialFileFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: MaterialFileSubmitValues) => Promise<void>;
}

export function MaterialFileForm({
  mode,
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<MaterialFileFormValues>();
  const rules = useMaterialFileRules(mode);

  const handleFinish = async ({ title, files }: MaterialFileFormValues) => {
    switch (mode) {
      case FormMode.Edit:
        await onSubmit({ title, file: files[0] }); // files[0] - can be undefined
        break;
      case FormMode.Creation:
        if (!files.length) {
          return;
        }

        await onSubmit({ title, file: files[0] });
        break;
    }
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
        <FileDropzone fileSizeLimitMb={FILE_SIZE_LIMIT_MB} />
      </Form.Item>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
