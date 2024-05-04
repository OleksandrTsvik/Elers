import { Form, FormInstance, Input } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import useCourseCreationRules from './use-course-creation.rules';
import { ErrorAlert } from '../../shared';

export interface CourseCreationFormValues {
  title: string;
  description: string;
}

interface Props {
  isError: boolean;
  error: unknown;
  onFormInstanceReady: (
    instance: FormInstance<CourseCreationFormValues>,
  ) => void;
  onSubmit: (values: CourseCreationFormValues) => Promise<void> | void;
}

export default function CourseCreationFrom({
  isError,
  error,
  onFormInstanceReady,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<CourseCreationFormValues>();
  const rules = useCourseCreationRules();

  useEffect(() => {
    onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  return (
    <Form form={form} layout="vertical" onFinish={onSubmit}>
      {isError && (
        <Form.Item>
          <ErrorAlert error={error} />
        </Form.Item>
      )}

      <Form.Item
        hasFeedback
        name="title"
        label={t('my_courses_page.form_title')}
        rules={rules.title}
      >
        <Input />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="description"
        label={t('my_courses_page.form_description')}
        rules={rules.description}
      >
        <Input.TextArea autoSize={{ minRows: 3, maxRows: 6 }} />
      </Form.Item>
    </Form>
  );
}
