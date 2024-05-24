import { Button } from 'antd';
import { Link } from 'react-router-dom';

import { TextEditorOutput } from '../../../common/typography';
import { CourseMaterial } from '../../../models/course-material.type';
import { CourseMaterialType } from '../../../shared';
import { handleDownloadFile } from '../../../utils/helpers';

interface Props {
  material: CourseMaterial;
}

export default function TabContentByType({ material }: Props) {
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
            handleDownloadFile(material.uniqueFileName, material.title)
          }
        >
          Click to download
          {material.title}
        </Button>
      );
  }
}
