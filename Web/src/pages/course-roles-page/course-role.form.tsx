import { Checkbox, Flex, Form, FormInstance, Input } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import useCourseRoleRules from './use-course-role.rules';
import { ErrorForm } from '../../common/error';
import { COURSE_ROLE_RULES } from '../../common/rules';
import { CoursePermissionListItem } from '../../models/course-permission.interface';

export interface CourseRoleFormValues {
  name: string;
  permissionIds: string[];
}

const defaultValues: CourseRoleFormValues = { name: '', permissionIds: [] };

interface Props {
  initialValues?: CourseRoleFormValues;
  permissions: CoursePermissionListItem[];
  error: unknown;
  onSubmit: (values: CourseRoleFormValues) => Promise<void>;
  onFormInstanceReady: (instance: FormInstance<CourseRoleFormValues>) => void;
}

export default function CourseRoleForm({
  initialValues,
  permissions,
  error,
  onSubmit,
  onFormInstanceReady,
}: Props) {
  const { t } = useTranslation();
  const initValues = initialValues ?? defaultValues;

  const [form] = Form.useForm<CourseRoleFormValues>();
  const rules = useCourseRoleRules();

  useEffect(() => {
    onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initValues}
      onFinish={onSubmit}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="name"
        label={t('course_roles_page.role')}
        rules={rules.name}
      >
        <Input showCount maxLength={COURSE_ROLE_RULES.name.max} />
      </Form.Item>

      <Form.Item
        name="permissionIds"
        label={t('course_roles_page.permissions')}
      >
        <Checkbox.Group>
          <Flex vertical>
            {permissions.map((item) => (
              <Checkbox key={item.id} value={item.id}>
                {item.description}
              </Checkbox>
            ))}
          </Flex>
        </Checkbox.Group>
      </Form.Item>
    </Form>
  );
}
