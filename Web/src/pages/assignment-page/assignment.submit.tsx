import { Button, Form, Typography, UploadFile } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import useAssignmentSubmitRules from './use-assignment-submit.rules';
import { useSubmitAssignmentMutation } from '../../api/assignments.api';
import { ErrorForm } from '../../common/error';
import { FileDropzone } from '../../common/file-upload';
import { TextEditor } from '../../common/typography';
import { FILE_SIZE_LIMIT_MB } from '../../utils/constants/app.constants';
import { getFileListFromEvent } from '../../utils/helpers';

interface AssignmentSubmitFormValues {
  text?: string;
  files?: UploadFile[];
}

interface Props {
  assignmentId: string;
  maxFiles: number;
}

export default function AssignmentSubmit({ assignmentId, maxFiles }: Props) {
  const { t } = useTranslation();

  const [emptyFieldsError, setEmptyFieldsError] = useState<string>();

  const [form] = Form.useForm<AssignmentSubmitFormValues>();
  const rules = useAssignmentSubmitRules();

  const [submitAssignment, { isLoading, error }] =
    useSubmitAssignmentMutation();

  const handleSubmit = async ({ text, files }: AssignmentSubmitFormValues) => {
    if (!text && !files?.length) {
      setEmptyFieldsError(
        maxFiles > 0
          ? t('assignment.empty_fields_error')
          : t('assignment.empty_text_error'),
      );
      return;
    } else {
      setEmptyFieldsError(undefined);
    }

    await submitAssignment({ id: assignmentId, text, files }).unwrap();
  };

  return (
    <Form form={form} layout="vertical" onFinish={handleSubmit}>
      <ErrorForm error={emptyFieldsError || error} form={form} />

      <Form.Item
        hasFeedback
        name="text"
        label={t('assignment.text')}
        rules={rules.text}
      >
        <TextEditor editorKey="text" />
      </Form.Item>

      {maxFiles > 0 && (
        <>
          <Form.Item
            hasFeedback
            name="files"
            label={t('assignment.files')}
            valuePropName="fileList"
            rules={rules.files}
            getValueFromEvent={getFileListFromEvent}
          >
            <FileDropzone
              multiple
              maxCount={maxFiles}
              fileSizeLimitMb={FILE_SIZE_LIMIT_MB}
            />
          </Form.Item>

          <Typography.Paragraph type="secondary">
            {t('assignment.max_files')}: {maxFiles}
          </Typography.Paragraph>
        </>
      )}

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {t('assignment.submit')}
        </Button>
      </Form.Item>
    </Form>
  );
}
