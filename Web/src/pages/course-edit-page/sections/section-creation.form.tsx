import { Button, Flex, Form, Input } from 'antd';
import { useTranslation } from 'react-i18next';

import useSectionCreationRules from './use-section-creation.rules';
import { useCreateCourseTabMutation } from '../../../api/courses.api';
import { CourseTab } from '../../../models/course.interface';
import { ErrorForm } from '../../../shared';
import { COURSE_RULES } from '../../../shared/rules';

interface FormValues {
  tabName: string;
}

interface Props {
  courseId: string;
  onCreationSection: (section: CourseTab) => void;
  onHide: () => void;
}

export default function SectionCreationForm({
  courseId,
  onCreationSection,
  onHide,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<FormValues>();
  const rules = useSectionCreationRules();

  const [createCourseTab, { isLoading, error }] = useCreateCourseTabMutation();

  const handleSubmit = async (values: FormValues) => {
    await createCourseTab({ courseId, name: values.tabName })
      .unwrap()
      .then((courseTab) => {
        onCreationSection(courseTab);
        onHide();
      });
  };

  return (
    <Form
      layout="vertical"
      form={form}
      initialValues={{ tabName: '' }}
      onFinish={handleSubmit}
    >
      <ErrorForm error={error} />

      <Form.Item
        hasFeedback
        name="tabName"
        label={t('course.section_name')}
        rules={rules.tabName}
      >
        <Input showCount maxLength={COURSE_RULES.tabName.max} />
      </Form.Item>
      <Flex justify="flex-end" gap="small">
        <Button danger type="primary" onClick={onHide}>
          {t('actions.cancel')}
        </Button>
        <Button
          className="btn-success"
          type="primary"
          htmlType="submit"
          loading={isLoading}
        >
          {t('course_edit_page.add_section')}
        </Button>
      </Flex>
    </Form>
  );
}
