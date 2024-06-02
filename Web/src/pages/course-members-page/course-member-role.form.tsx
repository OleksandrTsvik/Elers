import { Empty, Flex, Form, FormInstance, Radio, Skeleton } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import { useGetListCourseRolesQuery } from '../../api/course-roles.api';
import { ErrorForm } from '../../common/error';

export interface CourseMemberRoleFormValues {
  courseRoleId?: string;
}

interface Props {
  courseId?: string;
  initialValues: CourseMemberRoleFormValues;
  error: unknown;
  onSubmit: (values: CourseMemberRoleFormValues) => Promise<void>;
  onFormInstanceReady: (
    instance: FormInstance<CourseMemberRoleFormValues>,
  ) => void;
}

export default function CourseMemberRoleForm({
  courseId,
  initialValues,
  error,
  onSubmit,
  onFormInstanceReady,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<CourseMemberRoleFormValues>();

  const {
    data,
    isFetching,
    error: errorQuery,
  } = useGetListCourseRolesQuery({ courseId });

  useEffect(() => {
    onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  const handleRadioClick = () => {
    form.setFieldValue('courseRoleId', undefined);
  };

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={onSubmit}
    >
      <ErrorForm error={error || errorQuery} form={form} />

      {isFetching ? (
        <Skeleton />
      ) : (
        <Form.Item
          className="mb-0"
          name="courseRoleId"
          label={t('course_members_page.role')}
        >
          {data?.length ? (
            <Radio.Group>
              <Flex vertical>
                {data.map((item) => (
                  <Radio
                    key={item.id}
                    value={item.id}
                    onClick={handleRadioClick}
                  >
                    {item.name}
                  </Radio>
                ))}
              </Flex>
            </Radio.Group>
          ) : (
            <Empty description={t('course_members_page.no_roles')} />
          )}
        </Form.Item>
      )}
    </Form>
  );
}
