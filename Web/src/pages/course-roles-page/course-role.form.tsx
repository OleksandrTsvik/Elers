import {
  Checkbox,
  CheckboxProps,
  Flex,
  Form,
  FormInstance,
  Input,
  Typography,
} from 'antd';
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

  const formPermissionIds = Form.useWatch('permissionIds', form) ?? [];

  useEffect(() => {
    onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  const checkAllPermissions = permissions.length === formPermissionIds.length;

  const indeterminate =
    formPermissionIds.length > 0 &&
    formPermissionIds.length < permissions.length;

  const onCheckAllChange: CheckboxProps['onChange'] = (e) => {
    form.setFieldValue(
      'permissionIds',
      e.target.checked ? permissions.map(({ id }) => id) : [],
    );
  };

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

      <Typography.Paragraph className="m-0 pb-label-field">
        {t('course_roles_page.permissions')}
      </Typography.Paragraph>

      <Checkbox
        checked={checkAllPermissions}
        indeterminate={indeterminate}
        style={{ marginBottom: 8 }}
        onChange={onCheckAllChange}
      >
        {t('course_roles_page.select_all_permissions')}
      </Checkbox>

      <Form.Item name="permissionIds">
        <Checkbox.Group>
          <Flex vertical>
            {permissions.map(({ id, description }) => (
              <Checkbox key={id} value={id}>
                {description}
              </Checkbox>
            ))}
          </Flex>
        </Checkbox.Group>
      </Form.Item>
    </Form>
  );
}
