import { Form, FormInstance, Input } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import useCourseCreationRules from './use-course-creation.rules';
import { ErrorForm } from '../../common';
import { COURSE_RULES } from '../../common/rules';

export interface CourseCreationFormValues {
  title: string;
  description: string;
}

interface Props {
  error: unknown;
  onFormInstanceReady: (
    instance: FormInstance<CourseCreationFormValues>,
  ) => void;
  onSubmit: (values: CourseCreationFormValues) => Promise<void> | void;
}

export default function CourseCreationForm({
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
      <ErrorForm error={error} />

      <Form.Item
        hasFeedback
        name="title"
        label={t('course.title')}
        rules={rules.title}
      >
        <Input showCount maxLength={COURSE_RULES.title.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="description"
        label={t('course.description')}
        rules={rules.description}
      >
        <Input.TextArea
          showCount
          maxLength={COURSE_RULES.description.max}
          autoSize={{ minRows: 3, maxRows: 6 }}
        />
      </Form.Item>
    </Form>
  );
}
