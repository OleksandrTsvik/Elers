import { Button, Form, Input } from 'antd';
import { useTranslation } from 'react-i18next';

import { CourseMaterialIcon } from './course-material-type.icon';
import { CourseMaterialType } from './course-material.enum';
import useMaterialLinkRules from './use-material-link.rules';
import { ErrorForm } from '../../common/error';
import { COURSE_MATERIAL_RULES } from '../../common/rules';

export interface MaterialLinkFormValues {
  title: string;
  link: string;
}

interface Props {
  initialValues: MaterialLinkFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: MaterialLinkFormValues) => Promise<void> | void;
}

export function MaterialLinkForm({
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<MaterialLinkFormValues>();
  const rules = useMaterialLinkRules();

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={onSubmit}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="title"
        label={t('course_material.title')}
        rules={rules.title}
      >
        <Input showCount maxLength={COURSE_MATERIAL_RULES.link.title.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="link"
        label={t('course_material.link')}
        rules={rules.link}
      >
        <Input
          showCount
          maxLength={COURSE_MATERIAL_RULES.link.link.max}
          addonBefore={<CourseMaterialIcon type={CourseMaterialType.Link} />}
        />
      </Form.Item>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
