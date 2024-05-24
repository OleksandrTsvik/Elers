import { Button } from 'antd';
import { Link } from 'react-router-dom';

import { TextEditorOutput } from '../../../common/typography';
import { CourseMaterial } from '../../../models/course-material.type';
import {
  CourseMaterialType,
  useDownloadCourseMaterialFile,
} from '../../../shared';

interface Props {
  material: CourseMaterial;
}

export default function TabContentByType({ material }: Props) {
  const { downloadCourseMaterialFile } = useDownloadCourseMaterialFile();

  switch (material.type) {
    case CourseMaterialType.Content:
      return <TextEditorOutput text={material.content} />;
    case CourseMaterialType.Link:
      return (
        <Link to={material.link} target="_blank">
          {material.title}
        </Link>
      );
    case CourseMaterialType.File:
      return (
        <Button
          className="p-0"
          type="link"
          onClick={() =>
            downloadCourseMaterialFile(
              material.uniqueFileName,
              material.fileName,
            )
          }
        >
          {material.title}
        </Button>
      );
  }
}
